using Core.Entities;
using Core.Request;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> AddUser(User user);
    Task<User> GetUserByUsername(string username);
    
}