using ProductService.Api.Entities;

namespace ProductService.Api.Contracts;

public interface IProductBusiness
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    bool AddProduct(Product product);
}
