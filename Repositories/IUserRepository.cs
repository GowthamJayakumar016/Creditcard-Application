using Creditcard.Application.Api.Models;

namespace Creditcard.Application.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByIdAsync(int id);
    Task<bool> UserNameExistsAsync(string userName);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
