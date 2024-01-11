namespace TireHouse.Facade.Interface.Repository;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IProductRepository ProductRepository { get; }

    int SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
