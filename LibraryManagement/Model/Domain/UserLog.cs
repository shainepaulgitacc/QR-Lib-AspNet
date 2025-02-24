
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Domain
{
    public class UserLog
    {
        public int Id {get; set;}
        [ForeignKey("_user")]
        public string User { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public virtual User _user { get; set; }
    }
}
