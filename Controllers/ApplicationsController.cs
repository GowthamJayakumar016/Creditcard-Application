using System.Security.Claims;
using Creditcard.Application.Api.DTOs;
using Creditcard.Application.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Creditcard.Application.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Apply(ApplicationCreateDto dto)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _applicationService.ApplyAsync(userId.Value, dto);
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(result.Application);
    }

    [HttpGet("my")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> MyApplications()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _applicationService.GetByUserIdAsync(userId.Value);
        return Ok(result);
    }

    [HttpGet("my/{applicationNumber}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> MyApplicationStatus(string applicationNumber)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _applicationService.GetByApplicationNumberForUserAsync(userId.Value, applicationNumber);
        if (result == null)
        {
            return NotFound(new { message = "Application not found." });
        }

        return Ok(result);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AllApplications([FromQuery] string? status)
    {
        var result = await _applicationService.GetAllForAdminAsync(status);
        return Ok(result);
    }

    [HttpPatch("admin/{applicationNumber}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(string applicationNumber)
    {
        var result = await _applicationService.UpdateStatusAsync(applicationNumber, "Approved");
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message });
    }

    [HttpPatch("admin/{applicationNumber}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reject(string applicationNumber)
    {
        var result = await _applicationService.UpdateStatusAsync(applicationNumber, "Rejected");
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message });
    }

    private int? GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }
}
