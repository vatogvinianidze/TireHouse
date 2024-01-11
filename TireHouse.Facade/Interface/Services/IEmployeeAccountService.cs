namespace TireHouse.Facade.Interface.Services;

public interface IEmployeeAccountService : IAccountService
{
    void Register(int id, string username, string password);
}
