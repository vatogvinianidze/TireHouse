using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class ProductQueryService : QueryServiceBase<Product, IProductRepository>, IProductQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductQueryService(IUnitOfWork unitOfWork) : base(unitOfWork.ProductRepository)
    {
        _unitOfWork = unitOfWork;
    }
}
