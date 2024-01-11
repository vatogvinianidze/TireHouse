using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;

namespace TireHouse.Service;

public sealed class EmployeeCommandService : CommandServiceBase<Employee, IEmployeeRepository>, IEmployeeCommandService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeCommandService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.EmployeeRepository)
    {
        _unitOfWork = unitOfWork;
    }
}
