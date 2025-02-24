using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using LibraryManagement.Pages.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManagement.Pages.StudentArea
{
  
    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IUserAttendanceRepository _repoUserAttendance;
        private readonly IBorrowBookRepository _borBookRepo;
        private readonly IHubContext<BaseHub, IBaseHub> _baseHub;
        private readonly IUserRepository _repoUser;
        private readonly IBaseRepository<Book> _repoBook;
        public IndexModel(IUserAttendanceRepository repoAttendanceUser,
                          IMapper mapper,
                          IUserRepository repoUser,
                          IBorrowBookRepository borBookRepo,
                          IHubContext<BaseHub, IBaseHub> baseHub,
                          IBaseRepository<Book> repoBook)
        {
            _mapper = mapper;
            _repoUserAttendance= repoAttendanceUser;
            _borBookRepo= borBookRepo;
            _baseHub = baseHub;
            _repoUser = repoUser;
            _repoBook=repoBook;
        }

        public List<UsersAttendanceViewModel> UsersAttendance { get; set; }
        public List<BorrowedBooksViewModel> BorrowedBooks { get; set; }

        public int Books { get; set; }

        [BindProperty]
        public InputUserAttendanceModel InputModel { get; set; }

        [BindProperty]
        public string? UserId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
           var userAttendance = await _repoUserAttendance.UserAttendanceList();
           var borBooks = await _borBookRepo.BorrowBookList();
            var books = await _repoBook.GetAllRecords();
            Books = books.Count();
            UsersAttendance = userAttendance
                .Where(x => x.UserLog.TimeIn.Date == DateTime.Now.Date)
                .OrderByDescending(x => x.UserLog.Id)
                .ToList();
            BorrowedBooks = borBooks
                .Where(x => x.BorrowBook.BorrowTime.Date == DateTime.Now.Date)
                .OrderByDescending(x => x.BorrowBook.Id)
                .ToList();
            return Page();
        }
        public async Task<IActionResult> OnGetBookReturn(int Id)
        {
            var borBook = await _borBookRepo.GetOneRecord(Id);
            if (borBook == null)
                return BadRequest("invalid id");
            borBook.Returned = DateTime.Now;
            var result = borBook;
            await _borBookRepo.Update(Id, borBook);
            TempData["ValidationMessage"] = "successfully returned";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostBarrowScan()
        {
            var user = await _repoUser.GetOneRecord(UserId);
            if(user == null)
            {
                TempData["ValidationMessage"] = "invalid qr code/id";
                return RedirectToPage();
            }
            return RedirectToPage("./Borrow", new {UserId});
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                var getCurrentLog = await _repoUser.GetOneRecord(InputModel.User);
                if (getCurrentLog == null)
                {
                    TempData["ValidationMessage"] = "invalid qr code/id";
                    return RedirectToPage();
                }
                else
                {
                    var retreiveData = await _repoUserAttendance.GetCurrentLog(InputModel.User);
                    var usersAttendance = await _repoUserAttendance.GetAllRecords();
                    var countLogToday = usersAttendance.Where(x => x.TimeIn.Date == DateTime.Now.Date).Count();
                    if (retreiveData == null)
                    {
                        var converted = _mapper.Map<UserLog>(InputModel);
                        await _repoUserAttendance.Add(converted);
                        TempData["ValidationMessage"] = $"successfully login";

                    }
                    else
                    {
                        var convertedData = _mapper.Map<UserLog>(InputModel);
                        convertedData.TimeIn = retreiveData.TimeIn;
                        convertedData.TimeOut = DateTime.Now;
                        convertedData.Id = retreiveData.Id;
                        await _repoUserAttendance.Update(retreiveData.Id, convertedData);
                        _baseHub.Clients.All.ReceiveStAttendance(
                            getCurrentLog.FirstName + getCurrentLog.MiddleName + getCurrentLog.LastName,
                            retreiveData.TimeIn.ToString("hh:mm:ss tt"),
                            retreiveData.TimeOut?.ToString("hh:mm:ss tt"),
                            getCurrentLog.UserType.ToString(),
                            countLogToday.ToString());
                        TempData["ValidationMessage"] = "successfully logout";
                    }
                }

            }
            return RedirectToPage();

        }
       
    }
}
