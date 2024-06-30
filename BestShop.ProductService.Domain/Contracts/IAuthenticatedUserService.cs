
namespace BestShop.ProductService.Domain.Contracts;

public interface IAuthenticatedUserService
{
    Guid UserId { get; }
}
