using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace LibraryManagement.Pages.Application.AdminArea.StudentManagement
{
    [Authorize]
    public class UserRecordHistoryModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserAttendanceRepository _userAttRepo;
        private readonly IBorrowBookRepository _borBookRepo;
        public UserRecordHistoryModel(IUserRepository userRepo
            ,IUserAttendanceRepository userAttRepo,
            IBorrowBookRepository borBookRepo)
        {
            _userAttRepo = userAttRepo;
            _userRepo = userRepo;
            _borBookRepo = borBookRepo;
        }

        public User User { get; set; }
        public List<UserLog> LogHistory { get; set; }
        public List<BorrowedBooksViewModel> BorroweBookHistory { get; set; }
        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var user = await _userRepo.GetOneRecord(Id);

            if (user == null)
                return BadRequest($"{Id} invalid Id");
          
            User = user;
            var userLogs = await _userAttRepo.GetAllRecords();
            var borrowedRecords = await _borBookRepo.BorrowBookList();
            BorroweBookHistory = borrowedRecords
                .Where(x => x.User.UserId == Id)
                .OrderByDescending(x => x.BorrowBook.BorrowTime)
                .ToList();
            LogHistory = userLogs
                .Where(x => x.User == Id)
                .OrderByDescending(x => x.Id)
                .ToList();
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteLogin(int sId)
        {
            if (sId == 0)
                return BadRequest(sId);
           
            TempData["ValidationMessage"] = "successfully deleted";
            var userAttendance = await _userAttRepo.GetOne(sId);
            string Id = userAttendance?.User;
            await _userAttRepo.Delete(sId);
            return RedirectToPage("UserRecordHistory", new{ Id } );
        }
        public async Task<IActionResult>OnGetDeleteBorrow(int sId)
        {
            if (sId == 0)
                return BadRequest(sId);
           
            TempData["ValidationMessage"] = "successfully deleted";
            var borrowBook = await _borBookRepo.GetOneRecord(sId);
            string Id = borrowBook.UserId;
            await _borBookRepo.Delete(sId);
            return RedirectToPage("UserRecordHistory", new { Id });
        }
    }
}

