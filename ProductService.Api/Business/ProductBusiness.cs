using ProductService.Api.Contracts;
using ProductService.Api.Entities;
using ProductService.Api.Persistence.Contexts;

namespace ProductService.Api.Business;

public class ProductBusiness: IProductBusiness
{
    private readonly ApplicationDbContext _context;
    public ProductBusiness(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Product> GetProducts()
    {
        var products = _context.Products.ToList();
        return products;
    }

    public int GetProductsCount()
    {
        var cnt = _context.Products.Count();
        return cnt;
    }

    public Product GetProductById(int id)
    {
        var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
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
