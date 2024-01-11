using TireHouse.DTO;
using TireHouse.Facade.Interface.Repository;

namespace TireHouse.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(TireHouseDbContext context) : base(context) { }
}
