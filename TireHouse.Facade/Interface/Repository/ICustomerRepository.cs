using TireHouse.DTO;

namespace TireHouse.Facade.Interface.Repository;

public interface ICustomerRepository : IRepository<Customer>
{
    void DeleteAccount(Customer customer);
}
