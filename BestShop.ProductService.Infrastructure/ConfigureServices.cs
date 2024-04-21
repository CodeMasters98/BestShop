using BestShop.ProductService.Application.Contracts;
using BestShop.ProductService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BestShop.ProductService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));

        services.AddScoped<IProductService, BestShop.ProductService.Infrastructure.Implementation.ProductService>();

        return services;
    }
}
