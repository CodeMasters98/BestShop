using BestShop.ProductService.Domain.Contracts;

namespace BestShop.ProductService.Infrastructure.Persistence.Seeder
{
    public class ProductSeeder : IBaseSeeder<BestShop.ProductService.Domain.Entities.Product>
    {
        public IEnumerable<BestShop.ProductService.Domain.Entities.Product> GetSeedData()
            => new List<BestShop.ProductService.Domain.Entities.Product>()
                {
                    new()
                    {
                        BrandName = "test",
                        CreateAt = DateTime.Now,
                        Id = 1,
                        Name = "test",
                        Tags = null,
                        TotaPrice = 5000
                    }
                };
    }

}
