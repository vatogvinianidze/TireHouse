using System.Runtime.CompilerServices;

namespace TireHouse.Configuration;

public static class WebApplicationConfiguration
{
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
