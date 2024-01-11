using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Repositories;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(TireHouseDbContext context) : base(context) { }

    public void DeleteAccount(Customer customer)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));
        Account? account = _context.Account.Find(customer.Id);
        _context.Account.Remove(account!);
    }
}
