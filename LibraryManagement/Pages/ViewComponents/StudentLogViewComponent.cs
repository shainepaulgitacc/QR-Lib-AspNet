using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class StudentLogViewComponent: ViewComponent
    {
        public StudentLogViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View();
        }
    }
}
