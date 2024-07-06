using Core.Entities;
using Core.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _databaseContext.Users.ToListAsync();
    }
    public async Task<User> AddUser(User user)
    {
        await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _databaseContext.Users.FirstAsync(x => x.Username == username);
    }
}