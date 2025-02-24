using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public enum CurrentPage
    {
        StudentArea,
        EmployeeArea,
        AdminArea
    } 
    public class NavigationViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CurrentPage currentPage)
        {
            return View(currentPage);
        }
    }
}
