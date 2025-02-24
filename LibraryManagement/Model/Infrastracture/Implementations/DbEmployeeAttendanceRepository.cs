using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbEmployeeAttendanceRepository:DbBaseRepository<EmployeeAttendance>,IEmployeeAttendanceRepository
    {
        private readonly ApplicationDbContext _db;
        public DbEmployeeAttendanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<int>> Year2019ToNow()
        {
            
            var years = new List<int>();
            var currentYear = DateTime.Now.Year;
            while(currentYear >= DateTime.Now.AddYears(-4).Year)
            {
                years.Add(currentYear);
                currentYear -= 1;
            }
            return years;
        }

        public DateTime AttendanceWeek(DateTime date, WeeksInMonth wm)
        {
            var year = date.Year;
            var month = date.Month;

            switch (wm)
            {
                case WeeksInMonth.Week1:
                    return new DateTime(year, month, 1);
                case WeeksInMonth.Week2:
                    return new DateTime(year, month, 8);
                case WeeksInMonth.Week3:
                    return new DateTime(year, month, 15);
                case WeeksInMonth.Week4:
                    return new DateTime(year, month, 22);
                case WeeksInMonth.Week5:
                    return new DateTime(year, month, 29);
                default:
                    // Handle cases where an invalid enum value is provided.
                    throw new ArgumentOutOfRangeException(nameof(wm), "Invalid WeeksInMonth value provided");
            }
        }


        public async Task<IEnumerable<AttendanceThisWeek>> EmpAttendancePerWeek(DateTime? dt,WeeksInMonth wm)
        {
            var dateF = dt ?? DateTime.Now;
            var empAttendace = await EmployeeAttendance();
            var empAttendance = empAttendace.ToList()
                .OrderBy(ss => ss.EmpAttendance.TimeIn)
                .GroupBy(ss => ss.Employee.EmployeeId)
                .ToList();
            List<AttendanceThisWeek> filteredRec = new List<AttendanceThisWeek>();
            for(int i = 0;  i < empAttendance.Count; i++)
            {
                
                var att = empAttendance[i];

                var total = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM")).Count();
                double percentage = ((double)total/7)*100;
                var record = new AttendanceThisWeek()
                {
                    EmployeeId = att.Max(x => x.Employee.EmployeeId),
                    EmployeeName = $"{att.Max(x => x.Employee.FirstName)} {att.Max(x => x.Employee.LastName)}",
                    Sunday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Sunday).ToList(),
                    Monday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Monday).ToList(),
                    Tuesday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Tuesday).ToList(),
                    Wednesday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Wednesday).ToList(),
                    Thursday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Thursday).ToList(),
                    Friday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Friday).ToList(),
                    Saturday = att.Where(x => x.EmpAttendance.TimeIn >= AttendanceWeek(dateF, wm) && x.EmpAttendance.TimeIn <= AttendanceWeek(dateF, wm).AddDays(7) && x.EmpAttendance.TimeIn.ToString("yyyy/MMM") == dateF.ToString("yyyy/MMM") && x.EmpAttendance.TimeIn.DayOfWeek == DayOfWeek.Saturday).ToList(),
                    Present = total,
                    Percentage = percentage
                };
                filteredRec.Add(record);
            }
            return filteredRec;

        }
        public async Task<EmployeeAttendance?>CurrentLogRecord(string id)
        {
            var empAttList = await EmployeeAttendance();
            var empLoginNow = empAttList
            .Where(x => x.EmpAttendance.Employee == id 
                        && x.EmpAttendance.TimeIn.Date == DateTime.Today.Date
                        && x.EmpAttendance.TimeOut == null);
            if (empLoginNow.Count() == 0)
                return null;
            var maxId = empLoginNow.Max(x => x.EmpAttendance.Id);
            return await _db.EmpAttendance.FirstOrDefaultAsync(x => x.Id == maxId);
        }
        public async Task<IEnumerable<EmployeeAttendanceViewModel>> EmployeeAttendance()
        {
            return _db.Employees
                .Join(_db.EmpAttendance,
                employee => employee.EmployeeId,
                empAttendance => empAttendance.Employee,
                (employee, empAttendance) => new EmployeeAttendanceViewModel
                {
                    Employee = employee,
                    EmpAttendance = empAttendance,

                }).ToList();
        }


        public async Task Delete(int Id)
        {
            var empAttendance = await GetOne(Id);
            if (empAttendance != null)
            {
                _db.EmpAttendance.Remove(empAttendance);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<EmployeeAttendance> GetOne(int Id)
        {
            return await _db.EmpAttendance.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task Update(int Id, EmployeeAttendance model)
        {
            var empAttendance = await GetOne(Id);
            if (empAttendance != null)
            {
                _db.Entry(empAttendance).CurrentValues.SetValues(model);
                await _db.SaveChangesAsync();
            }
        }

    }
    public class AttendanceThisWeek
    {
        public DateTime Key { get; set; }
        public string EmployeeId { get; set; } 
        public string EmployeeName { get; set; }
        public List<EmployeeAttendanceViewModel>? Sunday { get; set; }
        public List<EmployeeAttendanceViewModel>? Monday { get; set; }
        public List<EmployeeAttendanceViewModel>? Tuesday { get; set; }
        public List<EmployeeAttendanceViewModel>? Wednesday { get; set; }
        public List<EmployeeAttendanceViewModel>? Thursday { get; set; }
        public List<EmployeeAttendanceViewModel>? Friday { get; set; }
        public List<EmployeeAttendanceViewModel>? Saturday { get; set; }
        public int Present { get; set; }
        public double Percentage { get; set; }
    }
}
