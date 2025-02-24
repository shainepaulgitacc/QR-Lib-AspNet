using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.ViewModel
{
    public class BorrowedBooksViewModel
    {
        public User User { get; set; }
        public BorrowedBooks BorrowBook { get; set; }
        public string CategoryName { get; set; }
        public Book Book { get; set; }
    }
}
