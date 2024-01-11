
using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(TireHouseDbContext context) : base(context) { }

    public void DeleteAccount(Employee employee)
    {
        if(employee == null) throw new ArgumentNullException(nameof(Customer));
        Account? account = _context.Account.Find(employee.Id);
        _context.Account.Remove(account!);
    }
}
