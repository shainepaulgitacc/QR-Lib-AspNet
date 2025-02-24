using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        Task<string> UploadProfile(IFormFile file);
    }
}
