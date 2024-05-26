
using BestShop.User.Application.DTOs;
using BestShop.User.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BestShop.User.Api.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("Login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        _authenticationService.Login(dto);
        return Ok();
    }


    [HttpPost]
    [Route("Register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        _authenticationService.Register(dto);
        return Ok();
    }
}
