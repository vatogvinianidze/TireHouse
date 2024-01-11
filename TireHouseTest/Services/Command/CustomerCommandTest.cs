using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;
using TireHouse.Service;

namespace TireHouseTest.Services.Command;

public class CustomerCommandTest : CommandTestBase
{
    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Insert(string firstname, string lastname, string username, string password)
    {
        Customer customer = GetTestRecord(firstname, lastname, username, password);
        CustomerCommandService customerCommand = new(_unitOfWork);
        customerCommand.Insert(customer);

        Assert.NotNull(customer.FirstName);
        Assert.NotNull(customer.LastName);
        Assert.True(customer.Id > 0);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void NotInserted(string firstname, string lastname, string username, string password)
    {
        Customer customer = GetTestRecord(firstname, lastname, username, password);
        customer.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            CustomerCommandService commandService = new(_unitOfWork);
            commandService.Insert(customer);
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Update(string firstname, string lastname, string username, string password)
    {
        Customer newCustomer = GetTestRecord(firstname, lastname, username, password);
        CustomerCommandService commandService = new(_unitOfWork);
        commandService.Insert(newCustomer);

        newCustomer.FirstName = $"New {firstname}";
        newCustomer.LastName = $"New {lastname}";
        commandService.Update(newCustomer);

        Customer updatedCustomer = _unitOfWork.CustomerRepository.Set(x => x.Id == newCustomer.Id).Single();

        Assert.NotNull(updatedCustomer);
        Assert.True(updatedCustomer.FirstName == newCustomer.FirstName &&
                    updatedCustomer.LastName == newCustomer.LastName);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void NotUpdated(string firstname, string lastname, string username, string password)
    {
        Customer newCustomer = GetTestRecord(firstname, lastname, username, password);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            CustomerCommandService customerCommand = new(_unitOfWork);
            customerCommand.Update(newCustomer);
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Delete(string firstname, string lastname, string username, string password)
    {
        Customer customer = GetTestRecord(firstname, lastname, username, password);
        CustomerCommandService commandService = new(_unitOfWork);
        commandService.Insert(customer);

        commandService.Delete(customer);

        Assert.True(_unitOfWork.CustomerRepository.Set(x => x.Id == customer.Id).Single().IsDeleted);
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
