using System.Linq.Expressions;

namespace TireHouse.Facade.Interface.Repository;

public interface IRepository <T> where T : class
{
    T Get(params object?[]? keyValue);
    IQueryable<T> Set(Expression<Func<T, bool>> predictae);
    IQueryable<T> Set();

    void Insert(T entity);
    void InsertRange(ICollection<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void Delete(params object?[]? keyValues);

}
