using TireHouse.DTO;
using TireHouse.Facade.GetHashString;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;
using TireHouse.Facade.LoginException;

namespace TireHouse.Service;

public sealed class EmployeeAccountService : IEmployeeAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeAccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Login(string username, string password)
    {
        if(string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
        if(string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        Employee? employee = _unitOfWork.EmployeeRepository
            .Set(x => x.Account != null &&
                      x.Account.UserName == username &&
                      x.Account.Password == password)
            .SingleOrDefault();

        if (employee == null)
            throw new LoginException(username);
    }

    public void Register(int id, string username, string password)
    {
        if(username == null) throw new ArgumentNullException(nameof(username));
        if(password == null) throw new ArgumentNullException(nameof(password));

        Employee employee = _unitOfWork.EmployeeRepository
            .Set(x => x.Id == id && !x.IsDeleted)
            .Single();

        employee.Account = new Account
        {
            UserName = username,
            Password = password
        };
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();
    }

    public void UpdatePassword(int id, string oldPassword, string newPassword)
    {
        if (oldPassword == null) throw new ArgumentNullException(nameof(oldPassword));
        if (newPassword == null) throw new ArgumentNullException(nameof(newPassword));

        Employee employee = _unitOfWork.EmployeeRepository
            .Set(x => x.Id == id && x.Account!.Password == oldPassword.GetHash() && !x.IsDeleted)
            .Single();

        employee.Account!.Password = newPassword.GetHash();
        _unitOfWork.EmployeeRepository.Update(employee);
        _unitOfWork.SaveChanges();
    }

    public void UnRegister(int customerId)
    {
        Employee employee = _unitOfWork.EmployeeRepository
            .Set(x => x.Id == customerId && !x.IsDeleted)
            .Single();

        _unitOfWork.EmployeeRepository.DeleteAccount(employee);
        _unitOfWork.SaveChanges();
    }

}
