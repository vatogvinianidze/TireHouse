using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TireHouse.Models;
using TireHouse.Facade.Interface.Services;
using TireHouse.DTO;

namespace TireHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly ICategoryCommandService _categoryCommandService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryQueryService categoryQueryService, ICategoryCommandService categoryCommandService, IMapper mapper)
        {
            _categoryQueryService = categoryQueryService;
            _categoryCommandService = categoryCommandService;
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        public CategoryModel Get(int id) => _mapper.Map<CategoryModel>(_categoryQueryService.Get(id));


        [HttpPost]
        public int Insert(CategoryModel model) => _categoryCommandService.Insert(_mapper.Map<Category>(model));

        [HttpPut("{id}")]
        public void Update(int id, CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            category.Id = id;
            _categoryCommandService.Update(category);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => _categoryCommandService.Delete(id);
    }
}
