using BestShop.User.Application.DTOs;
using static BestShop.User.Application.Interfaces.Wrappers;

namespace BestShop.User.Application.Interfaces;

public interface IAuthenticationService
{
    Task<Response<AuthenticationResponse>> Login(LoginDto dto);

    Task<Response<bool>> Register(RegisterDto dto);
}
