using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireHouse.DTO;
using TireHouse.Facade.Interface.Services;
using TireHouse.Models;

namespace TireHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        private readonly IMapper _mapper;

        public ProductController(IProductCommandService productCommandService, IProductQueryService productQueryService, IMapper mapper)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id) => _mapper.Map<ProductModel>(_productQueryService.Get(id));

        [HttpPost]
        public int Insert(ProductModel model) => _productCommandService.Insert(_mapper.Map<Product>(model));

        [HttpPut("{id}")]
        public void Update(int id, ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            product.Id = id;
            _productCommandService.Update(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _productCommandService.Delete(id);
    }
}
