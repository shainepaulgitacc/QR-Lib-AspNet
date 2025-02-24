using LibraryManagement.Model.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.ViewModel
{
    public class InputUserModel
    {
        [Required, DisplayName("User Id")]
        public string UserId { get; set; }
        [Required,DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required, DisplayName("Year Level")]
        public int YearLevel { get; set; }
        [DisplayName("Email"),EmailAddress]
        public string? Email { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required, DisplayName("Type of user")]
        public Users UserType { get; set; }
        public string? Course { get; set; }

        [DisplayName("Contact Number"),MinLength(11,ErrorMessage = "Required 11 digits")]
        public string? PhoneNumber { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
