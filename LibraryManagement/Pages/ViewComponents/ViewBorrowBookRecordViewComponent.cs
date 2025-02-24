using LibraryManagement.Model.Infrastracture.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class ViewBorrowBookRecordViewComponent: ViewComponent
    {
        private readonly IBorrowBookRepository _repo;
        public ViewBorrowBookRecordViewComponent(IBorrowBookRepository repo)
        {
            _repo= repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var borBooks = await _repo.BorrowBookList();
            var borBook = borBooks.FirstOrDefault(x => x.BorrowBook.Id == Id);
            return View(borBook);
        }
    }
}
