using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using LibraryManagement.Pages.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;

namespace LibraryManagement.Pages.Application.AdminArea
{
    public class AdminBasePageModel<T, T2> : PageModel
        where T: BaseModel
    {
        private readonly IBaseRepository<T> _repo;
        protected readonly IMapper _mapper;
        public AdminBasePageModel(IBaseRepository<T> baseRepo, IMapper mapper)
        {
            _repo = baseRepo;
            _mapper = mapper;
        }
        [TempData]
        public string validationMess { get; set; }
        public List<T> Records { get; set; }

        [BindProperty]
        public T2 InputModel { get; set; }
        [BindProperty]
        public RecordSelectedInputModel? selectedIds { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TempData["ValidationMessage"] = validationMess;
            var allRec = await _repo.GetAllRecords();
            Records = allRec.ToList();
            return Page();
        }
        public async Task<IActionResult> OnGetDelete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest(Id);
            await _repo.Delete(Id);
            validationMess = "successfully deleted";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostGenerateQRCode()
        {
            var Ids = selectedIds?.RecordIds;
            return RedirectToPage($"./{selectedIds?.PageRoute}", new { Ids });
        }
    }
}
