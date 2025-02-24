using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.BookCategoryManagement
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBaseRepository<Book> _bookRepo;
        private readonly IBaseRepository<BookCategory> _bCateg;
        private readonly IMapper _mapper;
        public IndexModel(IBaseRepository<Book> bookRepo,IBaseRepository<BookCategory> bCateg,IMapper mapper)
        {
            _bookRepo = bookRepo;
            _bCateg = bCateg;
            _mapper = mapper;
        }


        public List<BookViewModel> Books { get; set; }

        [BindProperty]
        public InputBookModel InputBook { get; set; }
        public async Task OnGetAsync()
        {
            var books = await _bookRepo.GetAllRecords();
            var bCategs = await _bCateg.GetAllRecords();
            var recordBooks = bCategs
                .Join(books,
                bCat => bCat.Id,
                book => book.BookCategoryId,
                (bCat, book) => new BookViewModel
                {
                    Category = bCat,
                    Book = book
                }).ToList();
            Books =  recordBooks;
        }
        public async Task<IActionResult>OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<Book>(InputBook);
            await _bookRepo.Add(converted);
            TempData["ValidationMessage"] = "successfully added";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<Book>(InputBook);
            await _bookRepo.Update(converted.Id.ToString(),converted);
            TempData["ValidationMessage"] = "successfully updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetDeleteIntId(int Id)
        {
            if (Id < 0)
                return NotFound();
            await _bookRepo.Delete(Id.ToString());
            TempData["ValidationMessage"] = "successfully deleted";
            return RedirectToPage();
        }
    }
}
