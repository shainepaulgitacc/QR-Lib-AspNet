using AutoMapper;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Pages.ViewComponents
{
    public class UpdateEmployeeViewComponent:ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _empRepository;

        public UpdateEmployeeViewComponent(IEmployeeRepository empRepository, IMapper mapper)
        {
            _mapper= mapper;
            _empRepository= empRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var getEmp = await _empRepository.GetOneRecord(Id);
            var converted = _mapper.Map<InputEmployeeModel>(getEmp);
            converted.LastUpdatedAt = DateTime.Now;
            return View(converted);
        }
    }
}
