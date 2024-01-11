namespace TireHouse.Facade.Interface.Services;

public interface IAccountService
{
    void Login(string usarname, string password);
    void UpdatePassword(int id, string oldPassword, string newPassword);
    void UnRegister(int customerId);
}
