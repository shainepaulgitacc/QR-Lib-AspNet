using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbUserRepository:DbBaseRepository<User>, IUserRepository
    {
        public readonly ApplicationDbContext _db;
        public DbUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BestUserModel>> BestUserRanking()
        {
            return _db.Users
                .GroupBy(x => x.UserId)
                .Select(result => new BestUserModel
                {
                    User = result.Max(x => x.FirstName) +" "+ result.Max(x => x.MiddleName) +" "+ result.Max(x => x.LastName),
                    LogCount = _db.UsersLogs.Where(x => x.User == result.Key && x.TimeIn.Date == DateTime.Now.Date).Count(),
                    UserId = result.Key,
                    UserType = result.Max(x => x.UserType)

                }).OrderByDescending(x => x.LogCount).ToList();
        }
    }
}
