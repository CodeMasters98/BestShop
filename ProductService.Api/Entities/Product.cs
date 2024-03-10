namespace ProductService.Api.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal TotaPrice { get; set; }
    public DateTime CreateAt { get; set; }
}
