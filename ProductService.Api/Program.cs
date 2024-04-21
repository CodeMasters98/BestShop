using Microsoft.EntityFrameworkCore;
using ProductService.Api;
using ProductService.Api.Middleware;
using ProductService.Api.Shared.Configs;
using BestShop.ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ProductDb");

// Add services to the container.
builder.Services
    .RegisterInfrastructureServices(connectionString)
    .RegisterPresentationServices();

//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.Configure<CustomSetting>(builder.Configuration.GetSection("CustomSetting"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseLogUrl();
    app.UseGlobalException();
}

app.MapControllers();

app.Run();
