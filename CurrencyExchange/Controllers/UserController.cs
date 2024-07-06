using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.UseCases;
using Core.Entities;
using Core.Request;
using Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CurrencyExchange.Controllers;

public class UserController : ControllerBase
{
    private readonly GetUsers _getUsers;
    private readonly AddUser _addUser;
    private readonly LoginUseCase _loginUseCase;
    private readonly IConfiguration _configuration;

    public UserController(GetUsers getUsers ,AddUser addUser,LoginUseCase loginUseCase, IConfiguration configuration)
    {
        _getUsers = getUsers;
        _addUser = addUser;
        _loginUseCase = loginUseCase;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var userId = await _loginUseCase.CheckLogin(loginRequest);
        if (userId != null)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]!),
                new Claim("UserId", userId.ToString() ?? string.Empty)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:ExpirationHour"]!)),
                signingCredentials: signIn);
            return Ok(new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), loginRequest.Username));
        }

        return Unauthorized();
    }

    [HttpGet("GetUsers")]
    [Authorize]
    public async Task<ActionResult<List<ShowUserData>>> GetAllUser()
    {
        return Ok(await _getUsers.GetAllUsers());
    }
    
    [HttpPost("AddUser")]
    public async Task<ActionResult<User>> AddUser(AddToUserTableDto addToUserTableDto)
    {
        return Ok(await _addUser.CreateNewUser(addToUserTableDto));
    }
}