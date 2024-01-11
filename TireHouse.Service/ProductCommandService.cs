using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class ProductCommandService : CommandServiceBase<Product, IProductRepository>, IProductCommandService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommandService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ProductRepository)
    {
        _unitOfWork = unitOfWork;
    }
}
