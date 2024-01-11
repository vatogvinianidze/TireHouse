using TireHouse.DTO;
using TireHouse.Facade.GetHashString;
using TireHouse.Service;

namespace TireHouseTest.Services.Query;

public class EmployeeQueryTest : QueryTestBase
{
    [Theory]
    [InlineData("Firstname 1", "Lastname 1", "Username 1", "Password 1")]
    [InlineData("Firstname 2", "Lastname 2", "Username 2", "Password 2")]
    [InlineData("Firstname 3", "Lastname 3", "Username 3", "Password 3")]
    [InlineData("Firstname 4", "Lastname 4", "Username 4", "Password 4")]
    public void Get(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        EmployeeQueryService queryService = new(_unitOfWork);

        var retrievedEmployee = queryService.Get(employee.Id);

        Assert.True(retrievedEmployee.Id == employee.Id);
    }

    [Theory]
    [InlineData("Firstname 1", "Lastname 1", "Username 1", "Password 1")]
    [InlineData("Firstname 2", "Lastname 2", "Username 2", "Password 2")]
    [InlineData("Firstname 3", "Lastname 3", "Username 3", "Password 3")]
    [InlineData("Firstname 4", "Lastname 4", "Username 4", "Password 4")]
    public void Set(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        List<Employee> exceptedSet = new();
        exceptedSet.Add(employee);

        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        EmployeeQueryService queryService = new(_unitOfWork);

        var retrievedEmployee = queryService.Set();

        Assert.True(exceptedSet.Last().FirstName == retrievedEmployee.Last().FirstName &&
                    exceptedSet.Last().LastName == retrievedEmployee.Last().LastName &&
                    exceptedSet.Last().Account!.UserName == retrievedEmployee.Last().Account!.UserName &&
                    exceptedSet.Last().Account!.Password.GetHash() == retrievedEmployee.Last().Account!.Password.GetHash());
    }

    [Theory]
    [InlineData("Firstname 1", "Lastname 1", "Username 1", "Password 1")]
    [InlineData("Firstname 2", "Lastname 2", "Username 2", "Password 2")]
    [InlineData("Firstname 3", "Lastname 3", "Username 3", "Password 3")]
    [InlineData("Firstname 4", "Lastname 4", "Username 4", "Password 4")]
    public void ExpressionSet(string firstname, string lastname, string username, string password)
    {
        Employee employee = GetTestRecord(firstname, lastname, username, password);
        List<Employee> exceptedSet = new();
        exceptedSet.Add(employee);

        _unitOfWork.EmployeeRepository.Insert(employee);
        _unitOfWork.SaveChanges();

        EmployeeQueryService queryService = new(_unitOfWork);

        var retrievedEmployee = queryService.Set(x => x.Id == employee.Id);

        Assert.NotNull(retrievedEmployee);
        Assert.True(exceptedSet.Last().FirstName == retrievedEmployee.Last().FirstName &&
                    exceptedSet.Last().FirstName == retrievedEmployee.Last().LastName &&
                    exceptedSet.Last().Account!.UserName == retrievedEmployee.Last().Account!.UserName &&
                    exceptedSet.Last().Account!.Password.GetHash() == retrievedEmployee.Last().Account!.Password.GetHash());
    }

    private static Employee GetTestRecord(string firstname, string lastname, string username, string password)
    {
        Employee employee = new()
        {
            FirstName = firstname,
            LastName = lastname,
            Account = new()
            {
                UserName = username,
                Password = password
            }
        };

        return employee;
    }
}
