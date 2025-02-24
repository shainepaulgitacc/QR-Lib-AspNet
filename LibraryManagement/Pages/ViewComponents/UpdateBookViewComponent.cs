using AutoMapper;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class UpdateBookViewComponent:ViewComponent
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;
        public UpdateBookViewComponent(IBookRepository bookRepo,IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var book = await _bookRepo.GetOneBook(Id);
            var converted = _mapper.Map<InputBookModel>(book);
            return View(converted);
        }

    }
}
