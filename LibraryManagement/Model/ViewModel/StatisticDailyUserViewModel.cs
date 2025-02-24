using LibraryManagement.Model.Domain;
using System.Xml.Schema;

namespace LibraryManagement.Model.ViewModel
{
    public class StatisticDailyUserViewModel
    {
        public string Course { get; set; }
        public List<int> TotalLogsPerday { get; set; }
        public int Total { get; set; }
    }
}
