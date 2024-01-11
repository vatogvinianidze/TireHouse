using Microsoft.EntityFrameworkCore;
using TireHouse.Facade.Interface.Repository;
using TireHouse.Facade.Interface.Services;
using TireHouse.Repositories;
using TireHouse.Service;

namespace TireHouse.Configuration;

public static class DefendencyConfigurationHelper
{
    public static void ConfigureDefendency(this WebApplicationBuilder builder, ConfigurationManager configuration)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddTransient<ICategoryQueryService, CategoryQueryService>();
        builder.Services.AddTransient<ICategoryCommandService, CategoryCommandService>();

        builder.Services.AddTransient<ICustomerQueryService, CustomerQueryService>();
        builder.Services.AddTransient<ICustomerCommandService, CustomerCommandService>();

        builder.Services.AddTransient<IEmployeeQueryService, EmployeeQueryService>();
        builder.Services.AddTransient<IEmployeeCommandService, EmployeeCommandService>();

        builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
        builder.Services.AddTransient<IProductCommandService, ProductCommandService>();

        builder.Services.AddTransient<ICustomerAccountService, CustomerAccountService>();
        builder.Services.AddTransient<IEmployeeAccountService, EmployeeAccountService>();

        builder.Services.AddDbContext<TireHouseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TireHouse")));
    }
}
