using TireHouse.DTO;
using TireHouse.Facade.GetHashString;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;
using TireHouse.Facade.LoginException;

namespace TireHouse.Service;

public sealed class CustomerAccountService : ICustomerAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerAccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Login(string username, string password)
    {
        if(string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        Customer? customer =_unitOfWork.CustomerRepository
            .Set(x => x.Account != null &&
                     x.Account.UserName == username &&
                     x.Account.Password == password.GetHash() &&
                     !x.IsDeleted)
            .SingleOrDefault();

        if (customer == null)
            throw new LoginException(username);
    }

    public void Register(string username, string password, Customer cutomer)
    {
        if(username == null) throw new ArgumentNullException(nameof(username));
        if (password == null) throw new ArgumentNullException(nameof(password));
        if(cutomer == null) throw new ArgumentNullException(nameof(cutomer));

        cutomer.Account = new Account
        {
            UserName = username,
            Password = password.GetHash()
        };
        _unitOfWork.CustomerRepository.Insert(cutomer);
        _unitOfWork.SaveChanges();
    }

    public void UpdatePassword(int id, string oldPassword, string newPassword)
    {
        if(oldPassword == null) throw new ArgumentNullException(nameof(oldPassword));
        if(newPassword == null) throw new ArgumentNullException(nameof(newPassword));

        Customer customer = _unitOfWork.CustomerRepository
            .Set(x => x.Id == id && x.Account.Password == oldPassword.GetHash() && !x.IsDeleted)
            .Single();

        customer.Account.Password = newPassword.GetHash();
        _unitOfWork.CustomerRepository.Update(customer);
        _unitOfWork.SaveChanges();
    }

    public void UnRegister(int customerId)
    {
        Customer customer = _unitOfWork.CustomerRepository
            .Set(x => x.Id == customerId && !x.IsDeleted)
            .Single();

        _unitOfWork.CustomerRepository.DeleteAccount(customer);
        _unitOfWork.SaveChanges();
    }
}
