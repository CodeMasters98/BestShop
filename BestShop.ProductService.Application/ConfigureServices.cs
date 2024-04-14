using Microsoft.Extensions.DependencyInjection;

namespace BestShop.ProductService.Application;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        return services;
    }
}
