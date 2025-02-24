using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Pages.Application.AdminArea.AccountManagement
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBaseRepository<IdentityUser> _accountRepo;
        public IndexModel(UserManager<IdentityUser> userManager,
                          IBaseRepository<IdentityUser> accountRepo,
                          SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _accountRepo = accountRepo;
            _signInManager = signInManager;
        }
        [BindProperty]
        public DeleteAccountInputModel DelAccInput { get; set; }
        public bool RequirePassword { get; set; }
        [BindProperty]
        public AddSubAdminAccountInputModel AddSubAdminInput { get; set; }
        public List<IdentityUser> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            var accounts = await _accountRepo.GetAllRecords();
            Accounts = accounts.Where(x => x.UserName != User.Identity?.Name).ToList();
        }
        public async Task<IActionResult> OnPostAddAdmin()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = new IdentityUser()
            {
                EmailConfirmed = true,
                Email = AddSubAdminInput.Email,
                UserName = AddSubAdminInput.Email,
            };
            var create = await _userManager.CreateAsync(account, AddSubAdminInput.Password);
            if (!create.Succeeded)
            {

                TempData["ValidationMessage"] = "can't add this account (make sure that you used unique email)";
                return RedirectToPage();
            }
            TempData["ValidationMessage"] = "successfully added";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAcc()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, DelAccInput.Password))
                {
                    TempData["ValidationMessage"] = "incorrect password";
                    return RedirectToPage();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }
            await _signInManager.SignOutAsync();
            return RedirectToPage();
        }
    }
}
