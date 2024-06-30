using BestShop.ProductService.Domain.Contracts;
using System.Security.Claims;

namespace ProductService.Api.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = new Guid(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"));
    }

    public Guid UserId { get; }
}

