using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.StatisticsDailyUser
{
    [Authorize]
    public class StatisticsByCategoryModel : PageModel
    {
        private readonly IBorrowBookRepository _borrowBookRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IEmployeeAttendanceRepository _empAttendanceRepository;
        public StatisticsByCategoryModel(IBorrowBookRepository borrowBookRepository,
                                         IBookCategoryRepository bCategory,
                                         IEmployeeAttendanceRepository empAttendanceRepository)
        {
            _borrowBookRepository = borrowBookRepository;
            _bookCategoryRepository = bCategory;
            _empAttendanceRepository = empAttendanceRepository;
        }


        [BindProperty]
        public StatisticOfBorrowBookInputModel InputModel { get; set; }

        [TempData]
        public int Year { get; set; }
        [TempData]
        public MonthsInYear MonthsInYear { get; set; }

        public List<StatisticsOfBookBorrowed> Records { get; set; }
        public List<BookCategory> BookCategories { get; set; }

        public List<int> Years { get; set; }
    
        public async Task OnGetAsync()
        {

            Years = await _empAttendanceRepository.Year2019ToNow();
            var currentMonth = new StatisticOfBorrowBookInputModel()
            {
                Month = (MonthsInYear)DateTime.Now.Month,
                Year = DateTime.Now.Year
            };
            if (Year <= 0 && MonthsInYear <= 0)
                InputModel = currentMonth;
            else
            {
                InputModel = new StatisticOfBorrowBookInputModel()
                { 
                    Year = this.Year,
                    Month = MonthsInYear
                };
            }
            var date = new DateTime(InputModel.Year, (int)InputModel.Month, 1);
            var borRec = await _borrowBookRepository.StudentBorrow(date);
            Records = borRec.ToList();

            var categories = await _bookCategoryRepository.GetAllRecords();
            var filtered = categories.OrderBy(x => x.CategoryName).ToList();
            BookCategories = filtered;

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
