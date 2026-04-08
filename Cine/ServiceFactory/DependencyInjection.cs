using Cine.BusinessLogic.Abstractions;
using Cine.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cine.BusinessLogic;
using Cine.Repository;

namespace Cine.ServiceFactory;

public static class DependencyInjection
{
    public static IServiceCollection AddCineDependencies(this IServiceCollection services, IConfiguration configuration)
    {
       /* var connectionString = configuration.GetConnectionString("CineDb");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<CineDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));
        }
        else
        {
            services.AddDbContext<CineDbContext>(options =>
                options.UseInMemoryDatabase("CineDb"));
        }*/

        services.AddSingleton<IMovieRepository, InMemoryMovieRepository>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}
