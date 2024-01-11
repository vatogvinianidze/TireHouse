using TireHouse.DTO;

namespace TireHouse.Facade.Interface.Repository;

public interface IEmployeeRepository : IRepository<Employee>
{
    void DeleteAccount(Employee employee);
}
