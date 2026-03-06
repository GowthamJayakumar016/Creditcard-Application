using Creditcard.Application.Api.DTOs;

namespace Creditcard.Application.Api.Services;

public interface IApplicationService
{
    Task<(bool Success, string Message, ApplicationResponseDto? Application)> ApplyAsync(int userId, ApplicationCreateDto dto);
    Task<List<ApplicationResponseDto>> GetByUserIdAsync(int userId);
    Task<ApplicationResponseDto?> GetByApplicationNumberForUserAsync(int userId, string applicationNumber);
    Task<List<AdminApplicationResponseDto>> GetAllForAdminAsync(string? status);
    Task<(bool Success, string Message)> UpdateStatusAsync(string applicationNumber, string status);
}
