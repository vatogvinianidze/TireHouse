using TireHouse.DTO;

namespace TireHouse.Facade.Interface.Services;

public interface ICustomerAccountService : IAccountService
{
    void Register(string username, string password, Customer cutomer);
}
