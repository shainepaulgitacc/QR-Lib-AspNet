using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class DashboardNavigationViewComponent: ViewComponent
    {
        public DashboardNavigationViewComponent() { }
        public async Task<IViewComponentResult> InvokeAsync(DNVModel model)
        {
            return View(model);
        }
    }
    public class DNVModel
    {
        public string PageName { get; set; }
        public (string linkName,string pageName,bool isCurrentPage)[] LinkName { get; set;}
    }
}
