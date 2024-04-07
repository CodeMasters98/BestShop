using ProductService.Api.Contracts;

namespace ProductService.Api.Entities;

public class Tag: BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}
