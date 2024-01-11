using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Repositories;

namespace TireHouseTest;

public static class Startup
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddDbContext<TireHouseDbContext>(options => options.UseInMemoryDatabase(databaseName: "MyTestDatabase"));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
