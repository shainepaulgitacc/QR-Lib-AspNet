using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.BorrowedBookManagement
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBorrowBookRepository _borBookRepo;
        public IndexModel(IBorrowBookRepository borBookRepo)
        {
            _borBookRepo = borBookRepo;
        }
        public List<BorrowedBooksViewModel> BorrowBookRecord { get; set; }
        public async Task OnGetAsync()
        {
            var borrowBooks = await _borBookRepo.BorrowBookList();
            BorrowBookRecord = borrowBooks.OrderByDescending(x => x.BorrowBook.Id).ToList();
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
        public async Task<IActionResult> OnGetDeleteIntId(int Id)
        {
            if(Id<0)
                return NotFound();
            await _borBookRepo.Delete(Id);
            TempData["ValidationMessage"] = "successfully deleted";
            return RedirectToPage();
        }
    }
}
