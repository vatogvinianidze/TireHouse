using Microsoft.Extensions.DependencyInjection;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Repositories;

namespace TireHouseTest.Repositories;

public abstract class RepositoryTestBase : UnitTestBase
{
    //protected readonly IUnitOfWork _unitOfWork;

    //public RepositoryTestBase()
    //{
    //    var serviceCollection = new ServiceCollection();
    //    Startup.ConfigureService(serviceCollection);
    //    var serviceProvider = serviceCollection.BuildServiceProvider();
    //    _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    //}
}
