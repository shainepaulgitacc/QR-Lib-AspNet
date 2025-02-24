using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IEmployeeAttendanceRepository:IBaseRepository<EmployeeAttendance>
    {
        Task<List<int>> Year2019ToNow();
        Task<IEnumerable<AttendanceThisWeek>> EmpAttendancePerWeek(DateTime? dt, WeeksInMonth wm);
        Task<IEnumerable<EmployeeAttendanceViewModel>> EmployeeAttendance();
        Task<EmployeeAttendance?> CurrentLogRecord(string id);

        Task Update(int id, EmployeeAttendance model);
        Task<EmployeeAttendance> GetOne(int Id);
        Task Delete(int Id);
    }
}
