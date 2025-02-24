using LibraryManagement.Model.Domain;

namespace LibraryManagement.Model.ViewModel
{
    public class StatisticOfBorrowBookInputModel
    {
        public int Year { get; set; }
        public MonthsInYear Month { get; set; }

    }
}
