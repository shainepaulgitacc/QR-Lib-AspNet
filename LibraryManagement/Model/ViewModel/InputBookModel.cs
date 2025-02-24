using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.ViewModel
{
    public class InputBookModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ClassNo { get; set; }
        [DisplayName("Accession No.")]
        public string? AccessionNumber { get; set; }
        [Required,DisplayName("Book Category/Class No")]
        public int BookCategoryId { get; set; }
        public string? Edition { get; set; }
        public string? Volume { get; set; }
        public string? Pages { get; set; }
        public string? Copies { get; set; }
        [DisplayName("Source Of Fund")]
        public string? SourceOfFund { get; set; }
        public string? Publisher { get; set; }
        public string? Year { get; set; }
    }
}
