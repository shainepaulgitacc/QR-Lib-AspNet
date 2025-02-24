using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Emit;

namespace LibraryManagement.Pages.Application.AdminArea.EmployeeManagement
{
    [Authorize]
    public class EmployeesQRCodeModel : PageModel
    {
        private readonly QRCode_Generator _qrGenerator;
        private readonly IEmployeeRepository _empRepo;
        public EmployeesQRCodeModel(QRCode_Generator qrGenertor, IEmployeeRepository empRepo)
        {
            _qrGenerator = qrGenertor;
            _empRepo = empRepo;
        }
        public List<GenerateQRCodesViewModel> QRCodes { get; set; }
        public async Task OnGetAsync(string Ids)
        {
            var viewModel = new List<GenerateQRCodesViewModel>();
            if (Ids != null)
            {
                string[] ids = Ids.Split(',');
                foreach (string id in ids)
                {
                    var getEmp = await _empRepo.GetOneRecord(id);
                    var qrcode = _qrGenerator.GenerateCode(id.ToString());
                    string imagebase64 = $"data:image/png;base64,{Convert.ToBase64String(qrcode)}";
                    viewModel.Add(new GenerateQRCodesViewModel
                    {
                        QRCode = imagebase64,
                        Name = getEmp.FirstName+getEmp.MiddleName+getEmp.LastName,
                        Id = getEmp.EmployeeId
                    });
                }
            }
            QRCodes = viewModel;
        }
    }
}
