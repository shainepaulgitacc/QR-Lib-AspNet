using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IBaseHub
    {
        Task ReceiveStAttendance(string name,string timeIn,string timeOut,string TypeOfUser,string countUserLogToday);
        Task ReceiveEmpAttendance(string name, string timeIn, string timeOut, string countEmpAttendanceToday);
    }
}
