using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IBookRepository:IBaseRepository<Book>
    {
        Task<Book> GetOneBook(int Id);
    }
}
