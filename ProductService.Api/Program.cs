using Microsoft.EntityFrameworkCore;
using ProductService.Api;
using ProductService.Api.Middleware;
using ProductService.Api.Persistence.Contexts;
using ProductService.Api.Shared.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterPresentationServices();

string connectionString = builder.Configuration.GetConnectionString("ProductDb");
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));

//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.Configure<CustomSetting>(builder.Configuration.GetSection("CustomSetting"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
