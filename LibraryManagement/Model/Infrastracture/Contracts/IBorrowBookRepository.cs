using LibraryManagement.Model.Domain;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IBorrowBookRepository:IBaseRepository<BorrowedBooks>
    {
        Task<IEnumerable<BorrowedBooksViewModel>> BorrowBookList();
        Task<BorrowedBooks> GetOneRecord(int Id);
        Task Delete(int Id);
        Task Update(int Id, BorrowedBooks model);
        Task<IEnumerable<StatisticsOfBookBorrowed>> StudentBorrow(DateTime date);
    }
}
