using Azure;
using Azure.Core;
using BestShop.User.Application.DTOs;
using BestShop.User.Application.Interfaces;
using BestShop.User.Domain.Settings;
using BestShop.User.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

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

    public async Task<AuthenticationResponse> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            return null;

        var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
            return null;
        var jwtToken = await GenerateJWToken(user);
        AuthenticationResponse response = new AuthenticationResponse();
        response.Id = user.Id;
        response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        response.Email = user.Email;
        response.UserName = user.UserName;
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;
        return response;
    }

    public async Task<bool> Register(RegisterDto dto)
    {
        var userWithSameUserName = await _userManager.FindByNameAsync(dto.Username);
        if (userWithSameUserName != null)
        {
            //$"Username '{request.UserName}' is already taken."
            throw new Exception();
        }
        var user = new ApplicationUser
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username
        };
        var userWithSameEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (userWithSameEmail == null)
        {
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                //var verificationUri = await SendVerificationEmail(user, origin);
                //TODO: Attach Email Service here and configure it via appsettings
                //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                //return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                throw new Exception();
            }
            else
            {
                throw new Exception();
            }
        }
        else
        {
            //$"Email {request.Email} is already registered."
            throw new Exception();
        }
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
