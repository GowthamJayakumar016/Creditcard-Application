using Creditcard.Application.Api.DTOs;

namespace Creditcard.Application.Api.Services;

public interface IAuthService
{
    Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto, string role = "User");
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}
