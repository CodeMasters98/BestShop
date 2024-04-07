#nullable disable

using ProductService.Api.Contracts;

namespace ProductService.Api.Entities;

public class Product: BaseEntity<int>
{
    public string Name { get; set; }
    public string BrandName { get; set; }
    public decimal TotaPrice { get; set; }

    public ICollection<Tag> Tags { get; set; }
}
