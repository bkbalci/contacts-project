using ContactService.Domain.Dto;
using ContactService.Domain.Entities;
using ContactService.Domain.Repositories;
using Mapster;

namespace ContactService.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<NoContent>> CreateUser(CreateUserDto request)
    {
        var name = request.Name.Trim();
        var surname = request.Surname.Trim();
        var isExists = await _userRepository.ExistAsync(x => x.Name == name && x.Surname == surname);
        if (isExists)
            return ApiResponse<NoContent>.Fail(401, "User already exists!");
        
        var user = request.Adapt<User>();
        await _userRepository.AddAsync(user);
        return ApiResponse<NoContent>.Success(201);
    }
}