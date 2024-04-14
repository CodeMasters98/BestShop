using ProductService.Api.Entities;

namespace ProductService.Api.Contracts;

public interface IProductBusiness
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProductById(int id);
    bool AddProduct(Product product);
}
