using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbBookCategoryRepository : DbBaseRepository<BookCategory>, IBookCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public DbBookCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task Delete(int Id)
        {
            var bookCategory = await GetOne(Id);
            if (bookCategory != null)
            {
               _db.BookCategories.Remove(bookCategory);
                await _db.SaveChangesAsync();   
            }
        }

        public async Task<BookCategory> GetOne(int Id)
        {
            return  await _db.BookCategories.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(int Id, BookCategory model)
        {
            var bookCategory = await GetOne(Id);
            if(bookCategory != null)
            {
                _db.BookCategories.Entry(bookCategory).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
