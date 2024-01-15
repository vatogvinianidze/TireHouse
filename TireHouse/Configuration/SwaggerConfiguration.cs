using Microsoft.OpenApi.Models;

namespace TireHouse.Configuration;

public static class SwaggerConfigurations
{
    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Your API",
                Version = "v1",
            });
        });
    }
}

