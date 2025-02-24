using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LibraryManagement.Pages.Application.AdminArea.EmployeeAttendance
{
    [Authorize]
    public class EmpAttendanceInputModel
    {
        public int Year { get; set; }
        public MonthsInYear Month { get; set; }
        public WeeksInMonth Week { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly IEmployeeAttendanceRepository _empRepo;
        public IndexModel(IEmployeeAttendanceRepository empRepo)
        {
            _empRepo = empRepo;
        }
        public List<int> Years { get; set; }

        [TempData]
        public int Year { get; set; }
        [TempData]
        public int Month { get; set; }
        [TempData]
        public WeeksInMonth Week { get; set; }

        [BindProperty]
        public EmpAttendanceInputModel InputModel { get; set; }
        public List<AttendanceThisWeek> AttendanceThisWeek { get; set; }
        public async Task OnGetAsync()
        {
            Years = await _empRepo.Year2019ToNow();
            IEnumerable<AttendanceThisWeek> attWeek;
            if (Year != 0 && Month != 0 && Week != null)
            {
                attWeek = await _empRepo.EmpAttendancePerWeek(new DateTime(Year,Month,1),Week);
                InputModel = new EmpAttendanceInputModel
                {
                    Year = Year,
                    Month = (MonthsInYear)Month,
                    Week = Week
                };
            }
            else
            {
                CultureInfo ci = CultureInfo.CurrentCulture;
                Calendar calendar = ci.Calendar;
                int weekNumberInMonth = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) -
                calendar.GetWeekOfYear(DateTime.Now.AddDays(1 - DateTime.Now.Day), CalendarWeekRule.FirstDay, DayOfWeek.Sunday) + 1;
                attWeek = await _empRepo.EmpAttendancePerWeek(null,(WeeksInMonth)weekNumberInMonth-2);
                InputModel = new EmpAttendanceInputModel
                {
                    Year = DateTime.Now.Year,
                    Month = (MonthsInYear)(DateTime.Now.Month),
                    Week = (WeeksInMonth)weekNumberInMonth - 2
                };
            }
            AttendanceThisWeek= attWeek.ToList();
        }
        public async Task<IActionResult> OnPostFilterAttendance()
        {
 
            Year = InputModel.Year;
            Month = (int)InputModel.Month;
            Week = InputModel.Week;
            return RedirectToPage();
        }
    }
}
