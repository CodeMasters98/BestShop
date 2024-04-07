#nullable disable

using ProductService.Api.Contracts;

namespace ProductService.Api.Entities;

public class User : BaseEntity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
}
