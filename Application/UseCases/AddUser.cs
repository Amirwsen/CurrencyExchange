using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases;

public class AddUser
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public AddUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    public async Task<ShowUserData> CreateNewUser(AddToUserTableDto addToUserTableDto)
    {
        var creatingUser = new User
        {
            Username = addToUserTableDto.Username,
            Email = addToUserTableDto.Email,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        creatingUser.Password = _passwordHasher.HashPassword(creatingUser, addToUserTableDto.Password);
        var newUser = await _userRepository.AddUser(creatingUser);
        return new ShowUserData
        {
            Id = newUser.Id,
            Email = newUser.Email,
            Username = newUser.Username,
            CreatedAt = newUser.CreatedAt,
            UpdatedAt = newUser.UpdatedAt
        };
    }
}