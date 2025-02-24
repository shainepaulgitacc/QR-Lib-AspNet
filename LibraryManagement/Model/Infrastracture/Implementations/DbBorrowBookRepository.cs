using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbBorrowBookRepository: DbBaseRepository<BorrowedBooks>,IBorrowBookRepository
    {
        private readonly ApplicationDbContext _db;
        public DbBorrowBookRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;
        }

        public async Task<IEnumerable<BorrowedBooksViewModel>> BorrowBookList()
        {
            var categories = await _db.BookCategories.ToListAsync();

            var retreived = _db.BorrowedBooks
                .Join(_db.Users,
                borBook => borBook.UserId,
                user => user.UserId,
                (borBook, user) => new
                {

                    BorrowBook = borBook,
                    User = user
                })
                .Join(_db.Books,
                result => result.BorrowBook.BookId,
                bookCateg => bookCateg.Id,
                (result, book) => new 
                {
                    User = result.User,
                    BorrowBook = result.BorrowBook,
                    Book= book,
                })
                .OrderByDescending(x => x.BorrowBook.Id)
                .ToList();
            return retreived
                .Join(categories,
                result => result.Book.BookCategoryId,
                categ => categ.Id,
                (result, categ) => new BorrowedBooksViewModel
                {
                    User = result.User,
                    Book = result.Book,
                    BorrowBook= result.BorrowBook,
                    CategoryName = categ.CategoryName
                });
        }
        public async Task Delete(int Id)
        {
            var borBooks = await GetOneRecord(Id);
            if (borBooks != null)
            {
               _db.BorrowedBooks.Remove(borBooks);  
                await _db.SaveChangesAsync();
            }
        }
        public async Task<BorrowedBooks> GetOneRecord(int Id)
        {
            return await _db.BorrowedBooks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(int Id, BorrowedBooks model)
        {
            var borBooks = await GetOneRecord(Id);
            if(borBooks != null)
            {
                _db.BorrowedBooks.Entry(borBooks).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<StatisticsOfBookBorrowed>>StudentBorrow(DateTime date)
        {
            var borrowedLists = await BorrowBookList();
            var students = borrowedLists.Where(x => x.User.UserType == Users.Student).ToList();
            var result = students
                .GroupBy(x => x.User.Course)
                .Select(async result => new StatisticsOfBookBorrowed()
                {
                    Course = result.Key,
                    CountBorrowed = await CountBorroweds(result.Key, date),
                    Total = students.Where(x => x.User.Course == result.Key
                                && x.BorrowBook.BorrowTime.Year == date.Year
                                && x.BorrowBook.BorrowTime.Month == date.Month)
                                    .Count()
                });
            var results = await Task.WhenAll(result);
            return results;
        }
        public async Task<List<int>> CountBorroweds(string course,DateTime date)
        {
            var categories = await _db.BookCategories
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
            var resultHandler = new List<int>();
            foreach (var category in categories)
            {
                var blist = await BorrowBookList();
                var count = blist
                    .Where(x => x.CategoryName == category.CategoryName 
                                && x.User.Course == course 
                                && x.BorrowBook.BorrowTime.Year == date.Year 
                                && x.BorrowBook.BorrowTime.Month == date.Month)
                    .Count();
                resultHandler.Add(count);
            }
            return resultHandler;
        }
    }
}
