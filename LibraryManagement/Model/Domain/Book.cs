using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Domain
{
    public class Book: BaseModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ClassNo { get; set; }
        public string? AccessionNumber { get; set; }
        [ForeignKey("BookCategory")]
        public int BookCategoryId { get; set; }
        public string? Edition { get; set; }
        public string? Volume { get; set; }
        public string? Pages { get; set; }
        public string? Copies { get; set; }
        public string? SourceOfFund { get; set; }
        public string? Publisher { get; set; }
        public string? Year { get; set; }

        public BookCategory BookCategory { get; set; }

    }
}
