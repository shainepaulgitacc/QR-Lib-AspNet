using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.ViewModel
{
    public class BestUserModel
    {
        public string User { get; set; }
        public int LogCount { get; set; }
        public string UserId { get; set; }
        public Users UserType { get; set; }
       
    }
}
