
using BestShop.ProductService.Domain.Entities;

namespace BestShop.ProductService.Application.Contracts;

public interface IProductService
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProductById(int id);
    bool AddProduct(Product product);
}
