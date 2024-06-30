using BestShop.ProductService.Application.Contracts;
using BestShop.ProductService.Infrastructure.Persistence.Contexts;
using BestShop.User.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BestShop.ProductService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));

        services.AddScoped<IProductService, BestShop.ProductService.Infrastructure.Implementation.ProductService>();

        return services;
    }

    public static IServiceCollection RegisterJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        JWTSettings settings = configuration.GetSection("JwtSetting").Get<JWTSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
            };
        });

        return services;
    }
}
