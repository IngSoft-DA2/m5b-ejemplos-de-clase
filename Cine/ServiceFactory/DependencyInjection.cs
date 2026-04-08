using Cine.BusinessLogic;
using Cine.BusinessLogic.Abstractions;
using Cine.Repository;
using Cine.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Cine.ServiceFactory;

public static class DependencyInjection
{
    public static IServiceCollection AddCineDependencies(this IServiceCollection services, IConfiguration configuration)
    {
   
        services.AddScoped<IMovieRepository, EfMovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}
