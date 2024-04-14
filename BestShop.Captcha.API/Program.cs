using BestShop.Captcha.API.Dtos;
using BestShop.Captcha.API.Entities;
using BestShop.Captcha.API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis 
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("CaptchaService"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/GenerateImage", async (AddCaptchaDto dto, ApplicationDbContext dbContext) =>
{
    Captcha captcha = new Captcha()
    {
        CreateAt = DateTime.Now,
        ValidUntil = DateTime.Now.AddMinutes(2),
        Key = dto.Key,
    };
    captcha.GenerateValue();

    dbContext.Captchas.Add(captcha);
    await dbContext.SaveChangesAsync();

    return Results.Created();
})
.WithName("GenerateImage")
.ProducesProblem(400)
.WithOpenApi();

app.Run();
