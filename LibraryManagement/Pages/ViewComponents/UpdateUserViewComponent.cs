using AutoMapper;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LibraryManagement.Pages.ViewComponents
{
    public class UpdateUserViewComponent: ViewComponent
    {
        private readonly IUserRepository _stRepository;
        private readonly IMapper _mapper;
        public UpdateUserViewComponent(IUserRepository stRepository, IMapper mapper)
        {
            _stRepository = stRepository;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var getStudent = await _stRepository.GetOneRecord(Id);
            var converted = _mapper.Map<InputUserModel>(getStudent);
            converted.LastUpdatedAt= DateTime.Now;
            return View(converted);
        }
    }
}
