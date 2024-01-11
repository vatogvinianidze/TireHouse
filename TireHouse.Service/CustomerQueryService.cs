using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class CustomerQueryService :  QueryServiceBase<Customer, ICustomerRepository>, ICustomerQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CustomerQueryService(IUnitOfWork unitOfWork) : base(unitOfWork.CustomerRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}
