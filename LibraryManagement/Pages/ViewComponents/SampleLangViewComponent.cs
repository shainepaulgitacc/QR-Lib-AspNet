using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class SampleLangViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string Sample)
        {
            return View(Sample);
        }
    }
}
