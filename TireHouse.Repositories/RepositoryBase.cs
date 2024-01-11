using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly TireHouseDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(TireHouseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }

    public T Get(params object?[]? keyValue) =>
        _dbSet.Find(keyValue) ?? throw new KeyNotFoundException($"{keyValue} Not Found");

    public IQueryable<T> Set(Expression<Func<T, bool>> predictae) =>
        _dbSet.Where(predictae);

    public IQueryable<T> Set() =>
        _dbSet;

    public void Insert(T entity) =>
        _dbSet.Add(entity);

    public void InsertRange(ICollection<T> entities) =>
        _dbSet.AddRange(entities);

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }


    public void Delete(params object?[]? keyValues) =>
        Delete(Get(keyValues));

    public void Delete(T entity)
    {
        if(_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
}
