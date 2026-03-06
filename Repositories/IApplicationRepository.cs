using Creditcard.Application.Api.Models;

namespace Creditcard.Application.Api.Repositories;

public interface IApplicationRepository
{
    Task AddAsync(Application application);
    Task<Application?> GetByApplicationNumberAsync(string applicationNumber);
    Task<List<Application>> GetByUserIdAsync(int userId);
    Task<List<Application>> GetAllAsync();
    Task<List<Application>> GetByStatusAsync(string status);
    Task<Application?> GetLatestByUserIdAsync(int userId);
    Task SaveChangesAsync();
}
