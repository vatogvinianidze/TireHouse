using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireHouse.DTO;
using TireHouse.Facade.Interface.Services;
using TireHouse.Models;

namespace TireHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        private readonly ICustomerCommandService _customerCommandService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerQueryService customerQueryService, ICustomerCommandService customerCommandService, IMapper mapper)
        {
            _customerCommandService = customerCommandService;
            _customerQueryService = customerQueryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public CustomerModel Get(int id) => _mapper.Map<CustomerModel>(_customerQueryService.Get(id));


        [HttpPost]
        public int Insert(CustomerModel model) => _customerCommandService.Insert(_mapper.Map<Customer>(model));


        [HttpPut("{id}")]
        public void Update(int id, CustomerModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            customer.Id = id;
            _customerCommandService.Update(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _customerCommandService.Delete(id);
    }
}
