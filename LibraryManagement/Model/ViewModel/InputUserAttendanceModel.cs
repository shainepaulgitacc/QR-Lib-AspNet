using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.ViewModel
{
    public class InputUserAttendanceModel
    {
        public int? Id { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string User { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
    }
}
