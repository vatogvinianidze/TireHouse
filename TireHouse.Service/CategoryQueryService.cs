using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public class CategoryQueryService : QueryServiceBase<Category, ICategoryRepository>, ICategoryQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryQueryService(IUnitOfWork unitOfWork) : base(unitOfWork.CategoryRepository)
    {
        _unitOfWork = unitOfWork;
    }
}