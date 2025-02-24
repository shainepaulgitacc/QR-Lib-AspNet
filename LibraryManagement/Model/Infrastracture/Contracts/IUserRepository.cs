
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<IEnumerable<BestUserModel>> BestUserRanking();
    }
}
