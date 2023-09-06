using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API;
// to create an extension method we need to create a static method
// a static class - we can use the methods without instantiating it
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // when we want something from our db, we need access to that dbcontext class,so we add it as a service to our program class
        services.AddDbContext<DataContext>(opt =>
        {
            // if intellisense is not present try ctrl + '.'
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        // add CORS Support
        services.AddCors();
        // add a service using the scoped method
        // we add the interface along with the scoped service
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
