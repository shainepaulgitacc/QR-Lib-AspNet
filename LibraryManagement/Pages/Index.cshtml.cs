using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IndexModel(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager)
        {
            _userManager= userManager;
            _signInManager= signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var isSignedIn = _signInManager.IsSignedIn(User);
            if (isSignedIn)
                return RedirectToPage("./Application/AdminArea/Index");
            return RedirectToPage("./Application/UserArea/Index");
        }
    }
}
