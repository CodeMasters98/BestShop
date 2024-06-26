﻿using BestShop.ProductService.Domain.Contracts;
using BestShop.ProductService.Domain.Entities;
using BestShop.ProductService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BestShop.ProductService.Infrastructure.Persistence.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Set Schema name
        builder.HasDefaultSchema("BASE");

        builder.Entity<Category>().Property<DateTime>("UpdateAt");
        builder.Entity<Tag>().Property<DateTime>("UpdateAt");

        builder.RegisterAllSeeders(typeof(IBaseSeeder<>).Assembly);

        //Atomate Rgeistartion configration
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //   => modelBuilder
    //       .HasDefaultSchema(EntitySchema.Ens)
    //       .RegisterAllEntities(typeof(EntityAttribute).Assembly)
    //       .RegisterAllSeeders(typeof(IBaseSeeder<>).Assembly)
    //       .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    public override int SaveChanges()
    {
        //Some change on my entities

        return base.SaveChanges();
    }

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
