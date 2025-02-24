using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.ViewModel
{
    public class InputBookCategoryModel
    {
        public int? Id { get; set; }
        [Required,DisplayName("Category Name")]
        public string CategoryName { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
