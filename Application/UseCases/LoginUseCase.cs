using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public LoginUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<Guid?> CheckLogin(LoginRequest request)
    {
        var user = await _userRepository.GetUserByUsername(request.Username);
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
        return verificationResult switch
        {
            PasswordVerificationResult.Failed => null,
            PasswordVerificationResult.Success => user.Id,
            _ => null
        };
    }
}