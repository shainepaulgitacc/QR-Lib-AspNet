namespace LibraryManagement.Model.ViewModel
{
    public class StatisticsOfBookBorrowed
    {
        public string Course { get; set; }
        public List<int> CountBorrowed { get; set; }
        public int Total { get; set; }
    }
}
