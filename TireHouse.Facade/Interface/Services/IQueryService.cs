using System.Linq.Expressions;

namespace TireHouse.Facade.Interface.Services;

public interface IQueryService<TEntity>
{
    TEntity Get(int id);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Set();
}

