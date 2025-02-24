using AutoMapper;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Application.AdminArea.UserManagement
{
    public class GuestManagementModel : UserManagementBasePageModel<User,InputUserModel>
    {
       public GuestManagementModel(IUserRepository repo,IMapper mapp):base(repo,mapp) { }
    }
}
