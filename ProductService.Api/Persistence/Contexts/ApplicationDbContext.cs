using Microsoft.EntityFrameworkCore;
using ProductService.Api.Entities;

namespace ProductService.Api.Persistence.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //data source=k2.liara.cloud,33504;Database=shopDb;User ID=sa;Password=hfa4HxYHKfFvrf5aAuj8OKAx;encrypt=false;Trust Server Certificate=true;
        //optionsBuilder.UseSqlServer(option => option.UseInMemoryDatabase(databaseName: "ProductDb"));
    }

    //public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    //data source=k2.liara.cloud,33504;Database=shopDb;User ID=sa;Password=hfa4HxYHKfFvrf5aAuj8OKAx;encrypt=false;Trust Server Certificate=true;
    //    //optionsBuilder.UseSqlServer(option => option.UseInMemoryDatabase(databaseName: "ProductDb"));
    //}

}
