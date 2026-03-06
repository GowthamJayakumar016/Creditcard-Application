using Creditcard.Application.Api.Data;
using Creditcard.Application.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Creditcard.Application.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByUserNameAsync(string userName)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<bool> UserNameExistsAsync(string userName)
    {
        return _context.Users.AnyAsync(u => u.UserName == userName);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
