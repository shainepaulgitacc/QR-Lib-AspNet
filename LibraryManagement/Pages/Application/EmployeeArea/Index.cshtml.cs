using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.Infrastracture.Services;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace LibraryManagement.Pages.Application.EmployeeArea
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeAttendanceRepository _empAttRepo;
        private readonly IEmployeeRepository _empRepo;
         private readonly IHubContext<BaseHub, IBaseHub> _baseHub;
        private readonly IMapper _mapper;
        public IndexModel(
            IEmployeeAttendanceRepository empAttRepo,
            IEmployeeRepository empRepo,
            IHubContext<BaseHub, IBaseHub> baseHub,
            IMapper mapper)
        {
            _mapper = mapper;
            _baseHub= baseHub;
            _empAttRepo = empAttRepo;
            _empRepo = empRepo;
        }
        [BindProperty]
        public InputEmpAttendanceModel InputModel { get; set; }
        public List<EmployeeAttendanceViewModel> EmployeeAttendanceRecord { get; set; }
        public async Task OnGetAsync()
        {
            var empAttRecords = await _empAttRepo.EmployeeAttendance();
            EmployeeAttendanceRecord = empAttRecords.Where(x => x.EmpAttendance.TimeIn.Date == DateTime.Now.Date).OrderByDescending(x => x.EmpAttendance.Id).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var getEmployee = await _empRepo.GetOneRecord(InputModel.Employee);
            if (getEmployee == null)
            {
                TempData["ValidationMessage"] = "invalid qr code";
                return RedirectToPage();
            }
            else
            {
                var currentLog = await _empAttRepo.CurrentLogRecord(InputModel.Employee);
                var empAttendances = await _empAttRepo.EmployeeAttendance();
                if (currentLog == null)
                {
                   
                    /*var empAttendance = empAttendances.Where(x => x.EmpAttendance.TimeOut?.Date == DateTime.Now.Date && x.EmpAttendance.Employee == InputModel.Employee).Count();
                    if(empAttendance > 0)
                    {
                        TempData["ValidationMessage"] = $"you're already logged in for the day";
                        return RedirectToPage();
                    }
                    */
                       
                    var converted = _mapper.Map<EmployeeAttendance>(InputModel);
                    await _empAttRepo.Add(converted);
                    TempData["ValidationMessage"] = $"successfully login";

                }
                else
                {
                    var convertedData = _mapper.Map<EmployeeAttendance>(InputModel);
                    var countEmpAttToday = empAttendances.Where(x => x.EmpAttendance.TimeIn.Date == DateTime.Now.Date).Count(); 
                    convertedData.TimeIn = currentLog.TimeIn;
                    convertedData.TimeOut = DateTime.Now;
                    convertedData.Id = currentLog.Id;
                    await _empAttRepo.Update(currentLog.Id, convertedData);
                    _baseHub.Clients.All.ReceiveEmpAttendance(
                        $"{getEmployee.FirstName} {getEmployee.LastName}",
                        currentLog.TimeIn.ToString("hh:mm:ss tt"),
                        currentLog.TimeOut?.ToString("hh:mm:ss tt"),
                        countEmpAttToday.ToString());
                    TempData["ValidationMessage"] = "successfully logout";
                }
            }

            return RedirectToPage();

        }
    }
}
