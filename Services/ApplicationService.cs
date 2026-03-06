using Creditcard.Application.Api.DTOs;
using Creditcard.Application.Api.Models;
using Creditcard.Application.Api.Repositories;

namespace Creditcard.Application.Api.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<(bool Success, string Message, ApplicationResponseDto? Application)> ApplyAsync(int userId, ApplicationCreateDto dto)
    {
        var latest = await _applicationRepository.GetLatestByUserIdAsync(userId);
        if (latest != null && latest.AppliedDate > DateTime.UtcNow.AddMonths(-6))
        {
            return (false, "User not eligible to apply within 6 months.", null);
        }

        var score = new Random().Next(600, 901);
        var (limit, status) = CalculateLimitAndStatus(dto.AnnualIncome);

        var entity = new Application
        {
            UserId = userId,
            Name = dto.Name,
            PanNumber = dto.PanNumber,
            DateOfBirth = dto.DateOfBirth,
            AnnualIncome = dto.AnnualIncome,
            CreditScore = score,
            CreditLimit = limit,
            Status = status,
            AppliedDate = DateTime.UtcNow,
            ApplicationNumber = $"APP-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}"
        };

        await _applicationRepository.AddAsync(entity);
        await _applicationRepository.SaveChangesAsync();

        return (true, "Application submitted.", MapToUserResponse(entity));
    }

    public async Task<List<ApplicationResponseDto>> GetByUserIdAsync(int userId)
    {
        var items = await _applicationRepository.GetByUserIdAsync(userId);
        return items.Select(MapToUserResponse).ToList();
    }

    public async Task<ApplicationResponseDto?> GetByApplicationNumberForUserAsync(int userId, string applicationNumber)
    {
        var item = await _applicationRepository.GetByApplicationNumberAsync(applicationNumber);
        if (item == null || item.UserId != userId)
        {
            return null;
        }

        return MapToUserResponse(item);
    }

    public async Task<List<AdminApplicationResponseDto>> GetAllForAdminAsync(string? status)
    {
        List<Application> items;
        if (string.IsNullOrWhiteSpace(status))
        {
            items = await _applicationRepository.GetAllAsync();
        }
        else
        {
            items = await _applicationRepository.GetByStatusAsync(status);
        }

        return items.Select(a => new AdminApplicationResponseDto
        {
            ApplicationNumber = a.ApplicationNumber,
            UserName = a.User.UserName,
            PanNumber = a.PanNumber,
            AnnualIncome = a.AnnualIncome,
            CreditScore = a.CreditScore,
            CreditLimit = a.CreditLimit,
            Status = a.Status,
            AppliedDate = a.AppliedDate
        }).ToList();
    }

    public async Task<(bool Success, string Message)> UpdateStatusAsync(string applicationNumber, string status)
    {
        if (status != "Approved" && status != "Rejected")
        {
            return (false, "Invalid status update.");
        }

        var item = await _applicationRepository.GetByApplicationNumberAsync(applicationNumber);
        if (item == null)
        {
            return (false, "Application not found.");
        }

        item.Status = status;
        await _applicationRepository.SaveChangesAsync();
        return (true, "Application status updated.");
    }

    private static ApplicationResponseDto MapToUserResponse(Application app)
    {
        return new ApplicationResponseDto
        {
            ApplicationNumber = app.ApplicationNumber,
            AppliedDate = app.AppliedDate,
            CreditScore = app.CreditScore,
            CreditLimit = app.CreditLimit,
            Status = app.Status
        };
    }

    private static (decimal? Limit, string Status) CalculateLimitAndStatus(decimal annualIncome)
    {
        if (annualIncome <= 200000)
        {
            return (50000, "Pending");
        }

        if (annualIncome <= 300000)
        {
            return (75000, "Pending");
        }

        if (annualIncome <= 500000)
        {
            return (100000, "Pending");
        }

        return (null, "ManualReview");
    }
}
