using Creditcard.Application.Api.Data;
using Creditcard.Application.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Creditcard.Application.Api.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;

    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Application application)
    {
        await _context.Applications.AddAsync(application);
    }

    public Task<Application?> GetByApplicationNumberAsync(string applicationNumber)
    {
        return _context.Applications
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.ApplicationNumber == applicationNumber);
    }

    public Task<List<Application>> GetByUserIdAsync(int userId)
    {
        return _context.Applications
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync();
    }

    public Task<List<Application>> GetAllAsync()
    {
        return _context.Applications
            .Include(a => a.User)
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync();
    }

    public Task<List<Application>> GetByStatusAsync(string status)
    {
        return _context.Applications
            .Include(a => a.User)
            .Where(a => a.Status == status)
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync();
    }

    public Task<Application?> GetLatestByUserIdAsync(int userId)
    {
        return _context.Applications
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AppliedDate)
            .FirstOrDefaultAsync();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
