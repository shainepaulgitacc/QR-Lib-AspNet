using LibraryManagement.Model.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.ViewModel
{
    public class InputBorrowedBooksModel
    {
        public int Id { get; set; }
        [Required]
        public string Book { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime BorrowTime { get; set; }
        public DateTime? Returned { get; set; }

    }
}
