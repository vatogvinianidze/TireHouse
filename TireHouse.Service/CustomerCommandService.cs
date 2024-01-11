using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class CustomerCommandService : CommandServiceBase<Customer, ICustomerRepository>, ICustomerCommandService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerCommandService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CustomerRepository)
    {
        _unitOfWork = unitOfWork;
    }
}
