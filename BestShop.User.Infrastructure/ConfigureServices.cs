using BestShop.User.Application.Interfaces;
using BestShop.User.Domain.Settings;
using BestShop.User.Infrastructure.Context;
using BestShop.User.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestShop.User.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,IConfiguration configuration, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));

        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
