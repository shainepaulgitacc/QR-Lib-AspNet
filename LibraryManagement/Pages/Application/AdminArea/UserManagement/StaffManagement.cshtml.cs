using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.UserManagement
{
    public class StaffManagementModel : UserManagementBasePageModel<User,InputUserModel>
    {
        public StaffManagementModel(IUserRepository userRepo, IMapper map) : base(userRepo, map)
        {

        }
    }
}
