using Microsoft.Extensions.DependencyInjection;
using TireHouse.Facade.Interface.Repository;

namespace TireHouseTest;

public abstract class UnitTestBase
{
    protected readonly IUnitOfWork _unitOfWork;

    public UnitTestBase()
    {
        var serviceCollection = new ServiceCollection();
        Startup.ConfigureService(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    }
}
