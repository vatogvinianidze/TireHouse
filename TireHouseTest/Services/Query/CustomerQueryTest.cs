using TireHouse.DTO;
using TireHouse.Facade.GetHashString;
using TireHouse.Service;

namespace TireHouseTest.Services.Query;

public class CustomerQueryTest : QueryTestBase
{
    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Get(string firstname, string lastname, string username, string password)
    {
        Customer newCustomer = GetTestRecord(firstname, lastname, username, password);
        _unitOfWork.CustomerRepository.Insert(newCustomer);
        _unitOfWork.SaveChanges();

        CustomerQueryService queryService = new(_unitOfWork);

        var retrievedCustomer = queryService.Get(newCustomer.Id);

        Assert.True(retrievedCustomer.Id == newCustomer.Id);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Set(string firstname, string lastname, string username, string password)
    {
        Customer customer = GetTestRecord(firstname, lastname, username, password);
        List<Customer> exceptedSet = new();
        exceptedSet.Add(customer);

        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        CustomerQueryService queryService = new(_unitOfWork);

        var retrievedCustomer = queryService.Set();

        Assert.True(exceptedSet.Last().FirstName == retrievedCustomer.Last().FirstName &&
                    exceptedSet.Last().LastName == retrievedCustomer.Last().LastName &&
                    exceptedSet.Last().Account.UserName == retrievedCustomer.Last().Account.UserName &&
                    exceptedSet.Last().Account.Password.GetHash() == retrievedCustomer.Last().Account.Password.GetHash());
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void ExpressionSet(string firstname, string lastname, string username, string password)
    {
        Customer customer = GetTestRecord(firstname, lastname, username, password);
        List<Customer> exceptedSet = new();
        exceptedSet.Add(customer);

        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        CustomerQueryService queryService = new(_unitOfWork);

        var retrievedCustomer = queryService.Set(x => x.Id == customer.Id);

        Assert.True(exceptedSet.Last().FirstName == retrievedCustomer.Last().FirstName &&
                    exceptedSet.Last().LastName == retrievedCustomer.Last().LastName &&
                    exceptedSet.Last().Account.UserName == retrievedCustomer.Last().Account.UserName &&
                    exceptedSet.Last().Account.Password.GetHash() == retrievedCustomer.Last().Account.Password.GetHash());
    }

    private static Customer GetTestRecord(string firstname, string lastname, string username, string password)
    {
        Customer customer = new()
        {
            FirstName = firstname,
            LastName = lastname,
            Account = new()
            {
                UserName = username,
                Password = password
            }
        };

        return customer;
    }

}
