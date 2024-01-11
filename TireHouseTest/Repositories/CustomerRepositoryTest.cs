using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;

namespace TireHouseTest.Repositories;

public class CustomerRepositoryTest : RepositoryTestBase
{
    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void Inserted(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        Assert.NotNull(customer.FirstName);
        Assert.True(customer.Id > 0);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void NotInserted(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        customer.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.SaveChanges();
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void Update(string firstname, string lastname)
    {
        Customer newCustomer = GetTestRecord(firstname, lastname);
        _unitOfWork.CustomerRepository.Insert(newCustomer);
        _unitOfWork.SaveChanges();

        newCustomer.FirstName = $"New {firstname}";
        newCustomer.LastName = $"New {lastname}";
        _unitOfWork.CustomerRepository.Update(newCustomer);
        _unitOfWork.SaveChanges();

        Customer UpdatedCustomer = _unitOfWork.CustomerRepository.Set(x => x.Id == newCustomer.Id).Single();

        Assert.NotNull(UpdatedCustomer);
        Assert.True(UpdatedCustomer.Id == newCustomer.Id);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void NotUpdated(string firstname, string lastname)
    {
        Customer newCustomer = GetTestRecord(firstname, lastname);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _unitOfWork.CustomerRepository.Update(newCustomer);
            _unitOfWork.SaveChanges();
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void DeleteByObject(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        _unitOfWork.CustomerRepository.Delete(customer);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.CustomerRepository.Set(x => x.Id == customer.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void DeleteById(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        _unitOfWork.CustomerRepository.Delete(customer.Id);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.CustomerRepository.Set(x => x.Id == customer.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void GetById(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        Customer retrievedCustomer = _unitOfWork.CustomerRepository.Get(customer.Id);

        Assert.True(retrievedCustomer.FirstName == customer.FirstName &&
            retrievedCustomer.LastName == customer.LastName);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void Set(string firstname, string lastname)
    {
        Customer customer = GetTestRecord(firstname, lastname);
        List<Customer> exceptedSet = new();
        exceptedSet.Add(customer);

        _unitOfWork.CustomerRepository.Insert(customer);
        _unitOfWork.SaveChanges();

        var retrievedCustomer = _unitOfWork.CustomerRepository.Set();

        Assert.True(exceptedSet.Last().FirstName == retrievedCustomer.Last().FirstName &&
            exceptedSet.Last().LastName == retrievedCustomer.Last().LastName);
    }

    private static Customer GetTestRecord(string firstname, string lastname)
    {
        Customer customer = new()
        {
            FirstName = firstname,
            LastName = lastname
        };

        return customer;
    }


}

