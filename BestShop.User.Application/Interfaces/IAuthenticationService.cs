using BestShop.User.Application.DTOs;

namespace BestShop.User.Application.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Login(LoginDto dto);
    Task<bool> Register(RegisterDto dto);
}
