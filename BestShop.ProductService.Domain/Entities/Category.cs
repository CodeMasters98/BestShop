#nullable disable


using BestShop.ProductService.Domain.Contracts;

namespace BestShop.ProductService.Domain.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
}
