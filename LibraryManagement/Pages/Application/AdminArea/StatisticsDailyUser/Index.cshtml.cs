using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LibraryManagement.Pages.Application.AdminArea.StatisticsDailyUser
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IEmployeeAttendanceRepository _repo;
        private readonly IUserAttendanceRepository _userAttRepo;
        public IndexModel(IEmployeeAttendanceRepository repo,
                          IUserAttendanceRepository userAttRepo)
        {
            _repo = repo;
            _userAttRepo = userAttRepo;
        }
        public List<int> Years { get; set; }
        public List<int> DaysInMonth { get; set; }
        public List<StatisticDailyUserViewModel> LogRecords { get; set; } 

        [BindProperty]
        public StatisticDailyUserInputModel InputModel { get; set; }
        [TempData]
        public int Year { get; set; }
        [TempData]
        public MonthsInYear MonthsInYear { get; set; }
        public async Task OnGetAsync()
        { 
            Years = await _repo.Year2019ToNow();
            var currentMonth = new StatisticDailyUserInputModel()
            {
                Month = (MonthsInYear)DateTime.Now.Month,
                Year = DateTime.Now.Year
            };
            if(Year <=0 && MonthsInYear <= 0)
                InputModel=  currentMonth;
            else
            {
                InputModel = new StatisticDailyUserInputModel()
                {
                    Year = this.Year,
                    Month = MonthsInYear
                };
            }

            var date = new DateTime(InputModel.Year,(int)InputModel.Month,1);
            var logRecords = await _userAttRepo.UsersLogs(date);
            LogRecords = logRecords.ToList();

            var current = date;
            var days = 1;
            var DaysInMonthHolder = new List<int>();
            while(current.Year <= date.Year && current.Month <= date.Month)
            {
                DaysInMonthHolder.Add(days++);
                current = current.AddDays(1);
            }
            DaysInMonth = DaysInMonthHolder;
            Year = 0;
            MonthsInYear = 0;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Year = InputModel.Year;
            MonthsInYear = InputModel.Month;
            return RedirectToPage();
        }

    }
}
