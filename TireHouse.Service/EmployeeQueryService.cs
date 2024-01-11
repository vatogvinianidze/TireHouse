using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class EmployeeQueryService : QueryServiceBase<Employee, IEmployeeRepository>, IEmployeeQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeQueryService(IUnitOfWork unitOfWork) : base(unitOfWork.EmployeeRepository)
    {
        _unitOfWork = unitOfWork;
    }
}
