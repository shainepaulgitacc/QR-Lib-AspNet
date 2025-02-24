using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.EmployeeManagement
{
    [Authorize]
    public class IndexModel : AdminBasePageModel<Employee,InputEmployeeModel>
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IMapper _mapper;
        public IndexModel(IEmployeeRepository empRepo,IMapper mapper):base(empRepo,mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnPostEmp()
        {
            if (!ModelState.IsValid)
                return BadRequest(InputModel);
            var employees = await _empRepo.GetAllRecords();
            var employee = employees.FirstOrDefault(x => x.EmployeeId == InputModel.EmployeeId);
            if (employee != null)
            {
                validationMess = "id already existing";
                TempData["ValidationMessage"] = validationMess;
                var allRec = await _empRepo.GetAllRecords();
                Records = allRec.ToList();
                return Page();
            }
            InputModel.ProfilePicture = await _empRepo.UploadProfile(InputModel.FProfilePicture) ?? null;
            var converted = _mapper.Map<Employee>(InputModel);
            await _empRepo.Add(converted);
            validationMess = "successfully added";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateEmp()
        {
            if (!ModelState.IsValid)
                return BadRequest(InputModel);
            InputModel.ProfilePicture = InputModel.FProfilePicture != null ? 
                await _empRepo.UploadProfile(InputModel.FProfilePicture) : 
                InputModel.ProfilePicture;
            var converted = _mapper.Map<Employee>(InputModel);
            await _empRepo.Update(converted.EmployeeId,converted);
            validationMess = "successfully updated";
            return RedirectToPage();
        }
        
    }
}
