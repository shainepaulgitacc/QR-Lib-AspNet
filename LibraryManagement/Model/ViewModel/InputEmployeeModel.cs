using LibraryManagement.Model.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.ViewModel
{
    public class InputEmployeeModel
    {
        [Required, DisplayName("Employee Id")]
        public string? EmployeeId { get; set; }
        [Required,DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [Required,DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Suffix(if any)")]
        public Suffix? Suffix { get; set; }
        [Required,DisplayName("Gender")]
        public Gender Gender { get; set; }
        [DisplayName("Profile Picture(Optional)")]
        public IFormFile? FProfilePicture { get; set; }
        public string? ProfilePicture { get; set; }
        
        [DisplayName("Position/Designation")]
        public string Designation { get; set; }
        [Required,DisplayName("Address")]
        public string Address { get; set; }
        [Required,DisplayName("Contact Number"),MinLength(11,ErrorMessage = "Required 11 digits")]
        public string ContactNumber { get; set; }
        [Required,DisplayName("Email Address"),EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
