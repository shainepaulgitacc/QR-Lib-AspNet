using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.UserArea
{
    public class BarrowModel : PageModel
    {
        public readonly IBaseRepository<BorrowedBooks> _barrowBookRepo;
        public readonly IBaseRepository<BookCategory> _bookCategRepo;
        public readonly IBaseRepository<Book> _bookRepo;
        public readonly IMapper _mapper;
        public BarrowModel(IBaseRepository<BorrowedBooks> barrowBookRepo,
                            IBaseRepository<BookCategory> bookCategRepo,
                            IBaseRepository<Book> bookRepo,
                           IMapper mapper)
        {
            _barrowBookRepo= barrowBookRepo;
            _bookCategRepo= bookCategRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        [BindProperty]
        public InputBorrowedBooksModel InputModel { get; set; }
        public List<BookCategory> BookCategories { get; set; }

        [BindProperty]
        public int? Categ { get; set; }
        [BindProperty]
        public string? Id { get; set; }

        [TempData]
        public int CategId { get; set; }

        [TempData]
        public string tId { get; set; }
        public List<Book> Books { get; set; }

        public string UserId { get; set; }
        public async Task OnGetAsync(string UserId)
        {
            var bookCategories = await _bookCategRepo.GetAllRecords();
            var books = await _bookRepo.GetAllRecords();
            this.UserId = UserId;
            if(CategId > 0)
            {
                Categ = CategId;
                Id = tId;

                InputModel = new InputBorrowedBooksModel
                {
                    UserId = tId
                };
                Books = books.Where(x => x.BookCategoryId == CategId).ToList();
            }
            else
            {
                Id = UserId;
                var firstCategoryId = bookCategories.First().Id;
                Books = books.Where(x => x.BookCategoryId == firstCategoryId).ToList();
            }
            BookCategories = bookCategories.ToList();

        }

        public async Task<IActionResult> OnPostFCategory()
        {
            CategId = (int)Categ;
            tId = Id;
            var userId = Id;
            return RedirectToPage("Borrow",new {userId});
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = await _bookRepo.GetAllRecords();
            var book = books.FirstOrDefault(x => x.Title == InputModel.Book);
            
            if(book == null)
            {
                var userId = InputModel.UserId;
                TempData["ValidationMessage"] = "invalid book";
                return RedirectToPage("Borrow", new { userId });
            }
            var bookId = book.Id;
            var values = new BorrowedBooks
            {
                BorrowTime = InputModel.BorrowTime,
                BookId = bookId,
                UserId = InputModel.UserId
            };
            await _barrowBookRepo.Add(values);
            TempData["ValidationMessage"] = "successfully borrowed";
            return RedirectToPage("./Index");
        }
    }
}
