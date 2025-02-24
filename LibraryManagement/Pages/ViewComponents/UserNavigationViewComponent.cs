using LibraryManagement.Model.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class UserNavigationViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Users user)
        {
            return View(user);
        }
    }
}
