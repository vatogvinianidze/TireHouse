using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;
using TireHouse.Service;

namespace TireHouseTest.Services.Command;

public class EmployeeCommandTest : CommandTestBase
{

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Insert(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        EmployeeCommandService employeeCommand = new(_unitOfWork);
        employeeCommand.Insert(employee);

        Assert.NotNull(employee.FirstName);
        Assert.NotNull(employee.LastName);
        Assert.True(employee.Id > 0);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void NotInserted(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        employee.Id = 1;

        Assert.Throws<ArgumentException>(() =>
        {
            EmployeeCommandService commandService = new(_unitOfWork);
            commandService.Insert(employee);
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Update(string firstname, string lastname, string username, string password)
    {
        Employee newEmployee = GetTestRecord(firstname, lastname, username, password);
        EmployeeCommandService commandService = new(_unitOfWork);
        commandService.Insert(newEmployee);

        newEmployee.LastName = $"New {firstname}";
        newEmployee.LastName = $"New {lastname}";
        newEmployee.Account!.UserName = $"New {username}";

        commandService.Update(newEmployee);

        Employee updatedEmployee = _unitOfWork.EmployeeRepository.Set(x => x.Id == newEmployee.Id).Single();

        Assert.NotNull(updatedEmployee);
        Assert.True(updatedEmployee.FirstName == newEmployee.FirstName &&
                    updatedEmployee.LastName == newEmployee.LastName);
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void NotUpdated(string firstname, string lastname, string username, string password)
    {
        Employee newEmployee = GetTestRecord(firstname, lastname, username, password);

        Assert.Throws<DbUpdateConcurrencyException>(() =>
        {
            EmployeeCommandService commandService = new(_unitOfWork);
            commandService.Update(newEmployee);
        });
    }

    [Theory]
    [InlineData("FirstName 1", "LastName 1", "Username 1", "Password 1")]
    [InlineData("FirstName 2", "LastName 2", "Username 2", "Password 2")]
    [InlineData("FirstName 3", "LastName 3", "Username 3", "Password 3")]
    [InlineData("FirstName 4", "LastName 4", "Username 4", "Password 4")]
    public void Delete(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        EmployeeCommandService employeeCommand = new(_unitOfWork);
        employeeCommand.Insert(employee);

        employeeCommand.Delete(employee);

        Assert.True(_unitOfWork.EmployeeRepository.Set(x => x.Id == employee.Id).Single().IsDeleted);
    }

    private static Employee GetTestRecord(string firstname, string lastname, string username, string password)
    {
        Employee employee = new()
        {
            FirstName = firstname,
            LastName = lastname,
            Account = new Account()
            {
                UserName = username,
                Password = password
            }
        };

        return employee;
    }
}
