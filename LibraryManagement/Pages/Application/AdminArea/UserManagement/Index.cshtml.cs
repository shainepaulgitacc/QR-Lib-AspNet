using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using LibraryManagement.Pages.Application.AdminArea;
using LibraryManagement.Pages.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.Application.AdminArea.UserManagement
{
    public class IndexModel : UserManagementBasePageModel<User,InputUserModel>
    {
        public IndexModel(IUserRepository userRepo, IMapper map) : base(userRepo, map)
        {
        }
    }
}
