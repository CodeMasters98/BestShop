using FluentValidation.AspNetCore;
using ProductService.Api.Contracts;
using ProductService.Api.Profiles;

namespace ProductService.Api
{
    public static class ConfigureService
    {
        public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductBusiness, ProductBusiness>();

            services.AddAutoMapper(typeof(ProductProfile));
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
