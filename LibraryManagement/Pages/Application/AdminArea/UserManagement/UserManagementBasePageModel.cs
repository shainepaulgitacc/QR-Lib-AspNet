using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.UserManagement
{
    [Authorize]
    public class UserManagementBasePageModel<T1,T2>:AdminBasePageModel<User,InputUserModel>
        where T1: User
        where T2: InputUserModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserManagementBasePageModel(IUserRepository userRepo, IMapper map):base(userRepo,map)
        {
            _userRepo = userRepo;
            _mapper = map;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var users = await _userRepo.GetAllRecords();
            var user = users.FirstOrDefault(x => x.UserId == InputModel.UserId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (user != null)
            {
                validationMess = "id already existing";
                TempData["ValidationMessage"] = validationMess;
                var allRec = await _userRepo.GetAllRecords();
                Records = allRec.ToList();
                return Page();
            }

            var converted = _mapper.Map<User>(InputModel);
            await _userRepo.Add(converted);
            validationMess = "successfully added";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var converted = _mapper.Map<User>(InputModel);
            await _userRepo.Update(converted.UserId, converted);
            validationMess = "successfully Updated";
            return RedirectToPage();
        }
    }
}
