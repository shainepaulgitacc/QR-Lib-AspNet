using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IBookCategoryRepository:IBaseRepository<BookCategory>
    {
        Task<BookCategory> GetOne(int Id);
        Task Delete(int Id);
        Task Update(int Id, BookCategory model);
    }
}
