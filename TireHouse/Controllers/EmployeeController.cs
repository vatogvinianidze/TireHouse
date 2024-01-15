using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireHouse.DTO;
using TireHouse.Facade.Interface.Services;
using TireHouse.Models;

namespace TireHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeCommandService _employeeCommandService;
        private readonly IEmployeeQueryService _employeeQueryService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeCommandService employeeCommandService, IEmployeeQueryService employeeQueryService, IMapper mapper)
        {
            _employeeCommandService = employeeCommandService;
            _employeeQueryService = employeeQueryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public EmployeeModel Get(int id) => _mapper.Map<EmployeeModel>(_employeeQueryService.Get(id));

        [HttpPost]
        public int Insert(EmployeeModel model) => _employeeCommandService.Insert(_mapper.Map<Employee>(model));

        [HttpPut("{id}")]
        public void Update(int id, EmployeeModel model)
        {
            var employee = _mapper.Map<Employee>(model);
            employee.Id = id;
            _employeeCommandService.Update(employee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _employeeCommandService.Delete(id);
    
    }
}
