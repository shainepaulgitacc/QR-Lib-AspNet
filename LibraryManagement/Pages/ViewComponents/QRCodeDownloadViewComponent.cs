using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class QRCodeDownloadViewComponent:ViewComponent
    {
        private readonly QRCode_Generator _qrGen;
        private readonly IUserRepository _userRepo;
        public QRCodeDownloadViewComponent(QRCode_Generator qrGen,IUserRepository userRepo)
        {
            _qrGen= qrGen;
            _userRepo= userRepo;
        }
        public async Task<IViewComponentResult>InvokeAsync(string Id)
        {
            var user = await _userRepo.GetOneRecord(Id);
            var qrCode = _qrGen.GenerateCode(Id.ToString());
            string imagebase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCode)}";
            ViewData["qr-code"] = imagebase64;
            ViewData["filename"] = $"{user.FirstName}{user.LastName}_QR-Code";
            return View();
        }
    }
}
