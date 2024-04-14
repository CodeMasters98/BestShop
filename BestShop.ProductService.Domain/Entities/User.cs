#nullable disable

using BestShop.ProductService.Domain.Contracts;

namespace BestShop.ProductService.Domain.Entities;

public class User : BaseEntity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
}
