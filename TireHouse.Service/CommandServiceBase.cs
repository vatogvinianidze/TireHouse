using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Service;

public abstract class CommandServiceBase<TEntity, TRepository> 
    where TEntity : class, IEntity
    where TRepository : IRepository<TEntity>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;

    public CommandServiceBase(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public virtual int Insert(TEntity entity)
    {
        if(entity == null) throw new ArgumentNullException(nameof(entity));

        _repository.Insert(entity);
        _unitOfWork.SaveChanges();

        return entity.Id;
    }

    public virtual void Update(TEntity entity)
    {
        if(entity == null) throw new ArgumentNullException(nameof(entity));

        _repository.Update(entity);
        _unitOfWork.SaveChanges();
    }

    public virtual void Delete(TEntity entity)
    {
        if(entity == null) throw new ArgumentNullException(nameof(entity));

        entity.IsDeleted = true;
        _repository.Update(entity);
        _unitOfWork.SaveChanges();
    }

    public virtual void Delete(int id) => Delete(_repository.Set(x => x.Id == id).Single());
}
