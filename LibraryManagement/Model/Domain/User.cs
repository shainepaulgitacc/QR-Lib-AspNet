using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.Domain
{
    public class User: BaseModel
    {
        [Key]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public int YearLevel { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
       
        public Gender Gender { get; set; }
        public Users UserType { get; set; }
        public string? Course { get; set; }
        public ICollection<UserLog> _userAttendance { get; set; }
        public ICollection<BorrowedBooks> _barrowedBooks { get; set; }  
    }
}
