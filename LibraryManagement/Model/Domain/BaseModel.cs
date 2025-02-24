namespace LibraryManagement.Model.Domain
{
    public class BaseModel
    {
        public DateTime AddedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; } 
    }
    public enum Gender
    {
        Male,
        Female,
    }
    public enum Designation
    {
        LibraryAssistant,
        ParaProfessional,
        NonMLS,
        Staff,
        ParaLibrarian
    }
    public enum Suffix
    {
        None,
        Jr,
        Sr,
        I,
        II,
        III

    }
    public enum DaysInWeek
    {
       Sunday,
       Monday,
       Tuesday,
       Wednesday,
       Thursday,
       Friday,
       Saturday
    }
    public enum WeeksInMonth
    {
        Week1,
        Week2,
        Week3,
        Week4,
        Week5
    }
    public enum Roles
    {
        Admin,
        Employee
    }
    public enum BookCategories
    {
        Category1,
        Category2,
        Category3,
        Category4,
        Category5,
        Category6,
        Category7,
        Category8,
        Category9,
        Category10,
    }
    public enum MonthsInYear
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum Users
    {
        Student,
        Faculty,
        Guest,
        Staff
    }
}
