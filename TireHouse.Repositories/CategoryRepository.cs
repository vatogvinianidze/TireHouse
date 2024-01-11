using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository 
{
    public CategoryRepository(TireHouseDbContext context) : base(context) { }
}
