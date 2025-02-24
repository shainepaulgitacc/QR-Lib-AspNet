using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace LibraryManagement.Pages.Application.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserAttendanceRepository _userAttRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmployeeAttendanceRepository _empAttRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IHubContext<BaseHub, IBaseHub> _baseHub;
        private readonly IBaseRepository<BookCategory> _bookCategRepo;
        private readonly IBorrowBookRepository _borBookRepo;
        private readonly IBaseRepository<Book> _bookRepo;
        public IndexModel(IUserAttendanceRepository userAttRepo,
                          IUserRepository userRepo,
                          IEmployeeAttendanceRepository empAttRepo,
                          IEmployeeRepository empRepo,
                          IBorrowBookRepository borrowBookRepo,
                          IBaseRepository<BookCategory> bookCategRepo
                          ,IBaseRepository<Book> bookRepo,
                          IHubContext<BaseHub, IBaseHub> baseHub)
        {
            _userAttRepo = userAttRepo;
           _borBookRepo = borrowBookRepo;
            _userRepo = userRepo;
            _empAttRepo = empAttRepo;
            _empRepo = empRepo;
            _bookCategRepo = bookCategRepo;
            _baseHub = baseHub;
            _bookRepo = bookRepo;
        }
        public List<UsersAttendanceViewModel> UsersLogs { get; set; }
        public List<EmployeeAttendanceViewModel>EmployeeAttendanceList { get; set; }
        public int CountBorrowToday { get; set; }
        public int CountEmployees { get; set; }
        public int CountUsers { get; set; }
        public int CountCategories { get; set; }
        public int CountEmployeesLoginToday { get; set; }
        public int CountUsersLoginToday { get; set; }
        public int CountBooks { get; set; }
        public double PercentageEmployeeLoginToday { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var usersLogs = await _userAttRepo.UserAttendanceList();
            var employeesAttendance = await _empAttRepo.EmployeeAttendance();
            var users = await _userRepo.GetAllRecords();
            var employees = await _empRepo.GetAllRecords();
            var categories = await _bookCategRepo.GetAllRecords();
            var books = await _bookRepo.GetAllRecords();
           var borBooks = await _borBookRepo.BorrowBookList();

           

            UsersLogs = usersLogs.Where(x => x.UserLog.TimeIn.Date == DateTime.Now.Date).ToList();
            EmployeeAttendanceList = employeesAttendance.Where(x => x.EmpAttendance.TimeIn.Date == DateTime.Now.Date).ToList();

            double percentageEmpLogToday = 0;
            double empAttendance = EmployeeAttendanceList.Count();
            double employee_Count = employees.Count(); 
            if(employees.Count()>0)
                percentageEmpLogToday = (empAttendance/employee_Count) * 100;

            CountEmployees = employees.Count();
            CountUsers= users.Count();
            CountCategories = categories.Count();
            CountBooks = books.Count();
            CountBorrowToday = borBooks.Where(x => x.BorrowBook.BorrowTime.Date == DateTime.Now.Date).Count();
            CountEmployeesLoginToday = EmployeeAttendanceList.Count();
            CountUsersLoginToday = UsersLogs.Count();
            PercentageEmployeeLoginToday = percentageEmpLogToday;
            return Page();
        }
    }
}
