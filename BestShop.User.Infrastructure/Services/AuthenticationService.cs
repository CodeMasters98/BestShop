using BestShop.User.Application.DTOs;
using BestShop.User.Application.Exceptions;
using BestShop.User.Application.Interfaces;
using BestShop.User.Domain.Settings;
using BestShop.User.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BestShop.User.Application.Interfaces.Wrappers;

namespace BestShop.User.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JWTSettings _jwtSettings;
    public AuthenticationService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }

    public async Task<Response<AuthenticationResponse>> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        //Response Pattern
        if (user is null)
            throw new ApiException($"user is bull please register!");

        var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
            throw new ApiException($"{result.Succeeded.ToString()}");

        var jwtToken = await GenerateJWToken(user);
        AuthenticationResponse response = new AuthenticationResponse();
        response.Id = user.Id;
        response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        response.Email = user.Email;
        response.UserName = user.UserName;
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;
        return new Response<AuthenticationResponse>(response);
    }

    public async Task<Response<bool>> Register(RegisterDto dto)
    {
        var userWithSameUserName = await _userManager.FindByNameAsync(dto.Username);
        if (userWithSameUserName != null)     
            throw new ApiException($"Username '{userWithSameUserName.UserName}' is already taken.");

        var user = new ApplicationUser
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username
        };
        var userWithSameEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (userWithSameEmail is null)
        {
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
                return new Response<bool>(true);
            else
                throw new ApiException(result.Succeeded.ToString());
        }
        else
            throw new ApiException($"Please enter valid email!");
    }

    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jaskdkdsfnksjdnfkjsdnfjksdnfjksdfnsjkdfnkjsdnfjsdjfksndfkjsnnjkasaskjdbnasjkd"));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

}
