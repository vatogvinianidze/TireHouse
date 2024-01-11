using System.Data;
using TireHouse.Facade.Interface.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace TireHouse.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TireHouseDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(TireHouseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(context));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
    }

    public ICategoryRepository CategoryRepository => _categoryRepository.Value;

    public ICustomerRepository CustomerRepository => _customerRepository.Value;

    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

    public IProductRepository ProductRepository => _productRepository.Value;

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
    public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();

    public void CommitTransaction()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }
    public void Dispose()
    {
        _context.Dispose();
        _transaction?.Dispose();
    }
}
