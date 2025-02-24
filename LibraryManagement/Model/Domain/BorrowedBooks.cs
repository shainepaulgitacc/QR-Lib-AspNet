using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Domain
{
    public class BorrowedBooks

    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("_book")]
        public int BookId { get; set; }

        [ForeignKey("_user")]
        public string UserId { get; set; }

        public DateTime BorrowTime { get; set; }

        public DateTime? Returned { get; set; }
        public virtual Book _book { get; set; }
        public virtual User _user { get; set; }
    }
}
