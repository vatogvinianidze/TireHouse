using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class CategoryCommandService : CommandServiceBase<Category, ICategoryRepository>, ICategoryCommandService
{
    public CategoryCommandService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CategoryRepository)
    {
    }
}
