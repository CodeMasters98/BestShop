
using BestShop.ProductService.Domain.Contracts;

namespace BestShop.ProductService.Domain.Entities;

public class Tag: BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}
