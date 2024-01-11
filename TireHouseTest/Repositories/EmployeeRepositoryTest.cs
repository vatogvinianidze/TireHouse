using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;

namespace TireHouseTest.Repositories;

public class EmployeeRepositoryTest : RepositoryTestBase
{
    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void Insert(string firstname, string lastname)
    {
        Employee employee = GetTestRecord(firstname, lastname);
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        Assert.NotNull(employee.FirstName);
        Assert.True(employee.Id > 0);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void NotInserted(string firstname, string lastname)
    {
        Employee employee = GetTestRecord(firstname, lastname);
        employee.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            _unitOfWork.EmployeeRepository.Insert(employee);
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
        Employee newEmployee = GetTestRecord(firstname, lastname);
        _unitOfWork.EmployeeRepository.Insert(newEmployee);
        _unitOfWork.SaveChanges();

        newEmployee.FirstName = $"New {firstname}";
        newEmployee.LastName = $"New {lastname}";
        _unitOfWork.EmployeeRepository.Update(newEmployee);
        _unitOfWork.SaveChanges();

        Employee updatedEmployee = _unitOfWork.EmployeeRepository.Set(x => x.Id == newEmployee.Id).Single();

        Assert.True(updatedEmployee.Id == newEmployee.Id);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void NotUpdated(string firstname, string lastname)
    {
        Employee newEmployee = GetTestRecord(firstname, lastname);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            _unitOfWork.EmployeeRepository.Update(newEmployee);
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
        Employee employee = GetTestRecord(firstname, lastname);
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        _unitOfWork.EmployeeRepository.Delete(employee);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.EmployeeRepository.Set(x => x.Id == employee.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void DeleteById(string firstname, string lastname)
    {
        Employee employee = GetTestRecord(firstname, lastname);
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        _unitOfWork.EmployeeRepository.Delete(employee.Id);
        _unitOfWork.SaveChanges();

        Assert.Null(_unitOfWork.EmployeeRepository.Set(x => x.Id == employee.Id).SingleOrDefault());
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void GetById(string firstname, string lastname)
    {
        Employee employee = GetTestRecord(firstname, lastname);
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        Employee retrievedEmployee = _unitOfWork.EmployeeRepository.Get(employee.Id);

        Assert.True(retrievedEmployee.Id == employee.Id);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1")]
    [InlineData("FirstName 2", "LastName 2")]
    [InlineData("FirstName 3", "LastName 3")]
    [InlineData("FirstName 4", "LastName 4")]
    public void Set(string firstname, string lastname)
    {
        Employee employee = GetTestRecord(firstname, lastname);
        List<Employee> exceptedSet = new();
        exceptedSet.Add(employee);

        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        var retrievedEmployee = _unitOfWork.EmployeeRepository.Set();

        Assert.True(exceptedSet.Last().FirstName == retrievedEmployee.Last().FirstName &&
            exceptedSet.Last().LastName == retrievedEmployee.Last().LastName);
    }

    private static Employee GetTestRecord(string firstname, string lastname)
    {
        Employee employee = new()
        {
            FirstName = firstname,
            LastName = lastname
        };

        return employee;
    }


}
