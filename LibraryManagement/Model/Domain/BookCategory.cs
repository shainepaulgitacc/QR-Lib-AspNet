using System.Drawing.Design;

namespace LibraryManagement.Model.Domain
{
    public class BookCategory : BaseModel
    {
        public int Id { get; set; } 
        public string CategoryName { get; set; }
        public virtual ICollection<BorrowedBooks> BarrowedBooks {get; set;} 
        public virtual ICollection<Book> Books { get; set;}
    }
}
