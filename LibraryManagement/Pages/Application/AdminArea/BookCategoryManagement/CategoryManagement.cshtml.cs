using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.BookCategoryManagement
{
    public class CategoryManagementModel : AdminBasePageModel<BookCategory,InputBookCategoryModel>
    {

        private readonly IBookCategoryRepository _bookCategoryRepo;
        private readonly IMapper _mapper;
        public CategoryManagementModel(IMapper mapper,IBookCategoryRepository bookCategoryRepo):base(bookCategoryRepo,mapper)
        {
            _bookCategoryRepo= bookCategoryRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<BookCategory>(InputModel);
            await _bookCategoryRepo.Add(converted);
            validationMess = "successfully added";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<BookCategory>(InputModel);
            await _bookCategoryRepo.Update(converted.Id,converted);
            validationMess = "successfully updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetDeleteIntId(int Id)
        {
            if (Id < 0)
                return BadRequest(Id);
            await _bookCategoryRepo.Delete(Id);
            validationMess = "successfully deleted";
            return RedirectToPage();
        }

    }
}
