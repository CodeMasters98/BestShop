using BestShop.ProductService.Application.Contracts;
using BestShop.ProductService.Domain.Entities;
using BestShop.ProductService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BestShop.ProductService.Infrastructure.Implementation;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public int GetProductsCount()
    {
        var cnt = _context.Products.Count();
        return cnt;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        return product;
    }

    public bool AddProduct(Product product)
    {
        //Console.WriteLine(_context.Entry(product).State);
        _context.Products.Add(product);
        //Console.WriteLine(_context.Entry(product).State);
        _context.SaveChanges();
        //Console.WriteLine(_context.Entry(product).State);
        return true;
    }
}
