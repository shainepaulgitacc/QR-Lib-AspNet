using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace LibraryManagement.Pages.Application.AdminArea.UserManagement
{
    public class FacultyManagementModel: UserManagementBasePageModel<User,InputUserModel>
    {
        public FacultyManagementModel(IUserRepository userRepo, IMapper map) : base(userRepo, map)
        {
        }
    }
}
