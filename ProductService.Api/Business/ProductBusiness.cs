using ProductService.Api.Contracts;
using ProductService.Api.Entities;

namespace ProductService.Api.Business;

public class ProductBusiness: IProductBusiness
{
    static List<Product> products = new List<Product>();
    public List<Product> GetProducts()
    {
        return products;
    }

    public Product GetProductById(int id)
    {
        var product = products.FirstOrDefault(x => x.Id == id);
        return product;
    }

    public bool AddProduct(Product product)
    {
        products.Add(product);
        return true;
    }
}
