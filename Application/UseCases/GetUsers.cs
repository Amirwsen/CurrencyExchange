using Application.DTOs;
using Core.Interfaces;

namespace Application.UseCases;

public class GetUsers
{
    private readonly IUserRepository _userRepository;

    public GetUsers(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<ShowUserData>> GetAllUsers()
    {
        return (await _userRepository.GetAllUsers()).Select(x => new ShowUserData
        {
            Username = x.Username,
            Email = x.Email
        }).ToList();
    }

}