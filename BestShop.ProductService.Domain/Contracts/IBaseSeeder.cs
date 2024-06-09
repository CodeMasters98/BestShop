namespace BestShop.ProductService.Domain.Contracts;

public interface IBaseSeeder<T>
{
    IEnumerable<T> GetSeedData();
}
