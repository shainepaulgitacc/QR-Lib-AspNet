using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Emit;

namespace LibraryManagement.Pages.Application.AdminArea.StudentManagement
{
    [Authorize]
    public class StudentsQRCodeModel : PageModel
    {
        private readonly QRCode_Generator _qrGenerator;
        private readonly IUserRepository _stRepo;
        public StudentsQRCodeModel(QRCode_Generator qrGenertor,IUserRepository stRepo)
        {
            _qrGenerator = qrGenertor;
            _stRepo = stRepo;
        }

        public List<GenerateQRCodesViewModel> QRCodes { get; set; }
        public async Task OnGetAsync(string Ids)
        {
            var viewModel = new List<GenerateQRCodesViewModel>();
            if(Ids != null)
            {
                string[] ids = Ids.Split(',');    
                foreach(string id in ids)
                {
                    var getStudent = await _stRepo.GetOneRecord(id);
                    var qrcode = _qrGenerator.GenerateCode(id.ToString());
                    string imagebase64 = $"data:image/png;base64,{Convert.ToBase64String(qrcode)}";
                    viewModel.Add(new GenerateQRCodesViewModel
                    {
                        QRCode = imagebase64,
                        Name = getStudent.FirstName + getStudent.MiddleName + getStudent.LastName,
                        Id = getStudent.UserId
                    });
                }
            }
            QRCodes = viewModel;
        }
    }
}
