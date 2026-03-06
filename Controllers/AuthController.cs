using Creditcard.Application.Api.DTOs;
using Creditcard.Application.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Creditcard.Application.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto, "User");
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message });
    }

    [HttpPost("admin/register")]
    public async Task<IActionResult> RegisterAdmin(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto, "Admin");
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (result == null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        return Ok(result);
    }
}
