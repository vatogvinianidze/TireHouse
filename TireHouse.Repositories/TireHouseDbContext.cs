using Microsoft.EntityFrameworkCore;
using TireHouse.DTO;

namespace TireHouse.Repositories;

public class TireHouseDbContext : DbContext
{
    public TireHouseDbContext(DbContextOptions<TireHouseDbContext> options) : base(options) { }

    public DbSet<Account> Account { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

}
