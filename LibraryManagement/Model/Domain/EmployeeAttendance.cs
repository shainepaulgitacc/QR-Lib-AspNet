using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Domain
{
    public class EmployeeAttendance
    {
        public int Id { get; set; }
        [ForeignKey("_employee")]
        public string Employee { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public virtual Employee _employee { get; set; }
    }
}
