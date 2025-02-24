using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbUserAttendanceRepository : DbBaseRepository<UserLog>, IUserAttendanceRepository
    {
        private readonly ApplicationDbContext _db;
        public DbUserAttendanceRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        
        public async Task<UserLog?> GetCurrentLog(string id)
        {
            var userAttList = await UserAttendanceList();
            var userLoginNow = userAttList
            .Where(x => x.UserLog.User == id && 
                   x.UserLog.TimeIn.Date== DateTime.Today.Date &&
                   x.UserLog.TimeOut == null);
            if (userLoginNow.Count() == 0)
                return null;
            var maxId = userLoginNow.Max(x => x.UserLog.Id); 
            return await _db.UsersLogs.FirstOrDefaultAsync(x => x.Id == maxId);
        }

        public async Task Delete(int Id)
        {
            var userlog = await GetOne(Id);
            if (userlog != null)
            {
                _db.UsersLogs.Remove(userlog);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<UserLog> GetOne(int Id)
        {
            return await _db.UsersLogs.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(int Id,UserLog model)
        {
            var userlog = await GetOne(Id);
            if(userlog != null)
            {
                _db.Entry(userlog).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<StatisticDailyUserViewModel>> UsersLogs(DateTime date)
        {
            var userLogs = await UserAttendanceList();
            var studentsLogs = userLogs.Where(x => x.User.UserType == Users.Student).ToList();

            var tasks = studentsLogs
                .GroupBy(user => user.User.Course)
                .Select(async userGroup => new StatisticDailyUserViewModel()
                {
                    Course = userGroup.Key,
                    TotalLogsPerday = await TotalLogsPerDay(userGroup.Key, date, userLogs.ToList()),
                    Total = userGroup.Where(x => x.User.Course == userGroup.Key && x.UserLog.TimeIn.Year == date.Year && x.UserLog.TimeIn.Month == date.Month).Count()
                });

            var results = await Task.WhenAll(tasks);

            return results;
        }
        public async Task<List<int>> TotalLogsPerDay(string course, DateTime date, List<UsersAttendanceViewModel> userLogs)
        {
            var resultHandler = new List<int>();
            var userByCourse = userLogs.Where(x => x.User.Course == course).ToList();
            var currentDay = date.Date;
            while (currentDay.Year <= date.Year && currentDay.Month <= date.Month)
            {
                var logs = userByCourse.Where(x => x.UserLog.TimeIn.Date == currentDay.Date).Count();
                resultHandler.Add(logs);
                currentDay = currentDay.AddDays(1);
            }
            return resultHandler.ToList();
        }

        /*
        public async Task<IEnumerable<StatisticDailyUserViewModel>> UsersLogs(DateTime date)
        {
            var userLogs = await UserAttendanceList();
            var studentsLogs = userLogs.Where(x => x.User.UserType == Users.Student).ToList();

            var tasks = studentsLogs
                .GroupBy(user => user.User.Course)
                .Select(async userGroup => new StatisticDailyUserViewModel()
                {
                    Course = userGroup.Key,
                    TotalLogsPerday = await TotalLogsPerDay(userGroup.Key, date)
                });

            var results = await Task.WhenAll(tasks);

            return results;
        }
        */
        public async Task<IEnumerable<UsersAttendanceViewModel>>UserAttendanceList()
        {
            return await _db.Users
                .Join(_db.UsersLogs,
                      user => user.UserId,
                      userLog => userLog.User,
                      (user, userLog) => new UsersAttendanceViewModel
                      {
                          User= user,
                         UserLog= userLog
                      })
                .OrderByDescending(x => x.UserLog.Id)
                .ToListAsync();
        }
        /*
        public async Task<List<int>> TotalLogsPerDay(string course,DateTime date)
        {
            var usersLogs = await UserAttendanceList();
            var resultHandler = new List<int>();
            var userByCourse = usersLogs.Where(x => x.User.Course == course).ToList();
            var currentDay = date.Date;
            while (currentDay.Year <= date.Year && currentDay.Month <= date.Month)
            {
                var logs = userByCourse.Where(x => x.UserLog.TimeIn.Date == date.Date).Count();
                resultHandler.Add(logs);
                currentDay = currentDay.AddDays(1);
            }
            return resultHandler.ToList();
        }*/
    }
}
