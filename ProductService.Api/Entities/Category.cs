#nullable disable

using ProductService.Api.Contracts;

namespace ProductService.Api.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
}
