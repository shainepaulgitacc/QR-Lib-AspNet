using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.ViewModel
{
    public class DeleteAccountInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
