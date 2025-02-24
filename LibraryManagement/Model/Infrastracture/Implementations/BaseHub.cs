using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class BaseHub: Hub<IBaseHub>  
    {
        public async Task SendStudentAttendance(string stName,string timeIn,string timeOut,string TypeOfUser,string countUserLogToday)
        {
            await Clients.All.ReceiveStAttendance(stName, timeIn, timeOut,TypeOfUser,countUserLogToday);
        }
        public async Task SendEmpAttendance(string empName, string timeIn, string timeOut,string countEmpAttToday)
        {
            await Clients.All.ReceiveEmpAttendance(empName, timeIn, timeOut,countEmpAttToday);
        }
    }
}
