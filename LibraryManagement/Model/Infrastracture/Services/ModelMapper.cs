using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.ViewModel;

namespace LibraryManagement.Model.Infrastracture.Services
{
    public class ModelMapper: Profile
    {
        public ModelMapper()
        {
            CreateMap<InputUserAttendanceModel, UserLog>().ReverseMap();
            CreateMap<InputUserModel,User>().ReverseMap();
            CreateMap<InputEmpAttendanceModel, EmployeeAttendance>();
            CreateMap<InputEmployeeModel, Employee>().ReverseMap();
            CreateMap<InputBookCategoryModel,BookCategory>().ReverseMap();
            CreateMap<InputBorrowedBooksModel,BorrowedBooks>().ReverseMap();
            CreateMap<InputBookModel,Book>().ReverseMap();
        }
       
    }
}
