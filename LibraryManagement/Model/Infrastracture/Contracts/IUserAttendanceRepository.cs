using LibraryManagement.Model.Domain;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IUserAttendanceRepository: IBaseRepository<UserLog>
    {
        Task<IEnumerable<UsersAttendanceViewModel>> UserAttendanceList();
        Task<IEnumerable<StatisticDailyUserViewModel>> UsersLogs(DateTime date);
        Task<UserLog?> GetCurrentLog(string id);

        Task Update(int id,UserLog model);
        Task<UserLog> GetOne(int Id);
        Task Delete(int Id);
    }
}
