using BestShop.ProductService.Application.Profiles;
using FluentValidation.AspNetCore;

namespace ProductService.Api
{
    public static class ConfigureService
    {
        public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
