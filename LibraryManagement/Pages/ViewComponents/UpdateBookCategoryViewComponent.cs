using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class UpdateBookCategoryViewComponent: ViewComponent
    {
        private readonly IBookCategoryRepository _bookCategoryRepo;
        private readonly IMapper _mapper;
        public UpdateBookCategoryViewComponent(IMapper mapper,IBookCategoryRepository bookCategoryRepo)
        {
            _mapper = mapper;
            _bookCategoryRepo = bookCategoryRepo;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var bookCategory = await _bookCategoryRepo.GetOne(Id);
            var converted = _mapper.Map<InputBookCategoryModel>(bookCategory);
            return View(converted);
        }
    }
}
