using ContactService.Domain.Dto;
using ContactService.Domain.Entities;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContactInfoRepository _userContactInfoRepository;

    public UserService(IUserRepository userRepository, IUserContactInfoRepository userContactInfoRepository)
    {
        _userRepository = userRepository;
        _userContactInfoRepository = userContactInfoRepository;
    }
    
    public async Task<ApiResponse<List<UserDto>>> GetUsers()
    {
        var users = await _userRepository.GetListAsync(x => true);
        var usersDto = users.Adapt<List<UserDto>>();
        return ApiResponse<List<UserDto>>.Success(200, usersDto);
    }
    
    public async Task<ApiResponse<UserDto>> GetUserById(Guid uuid)
    {
        var user = await _userRepository.GetAsync(x => x.UUID == uuid, includes: x => x.Include(y => y.ContactInfos));
        if (user == null)
            return ApiResponse<UserDto>.Fail(404, "User not found!");
        var userDto = user.Adapt<UserDto>();
        return ApiResponse<UserDto>.Success(200, userDto);
    }


    public async Task<ApiResponse<NoContent>> CreateUser(CreateUserDto request)
    {
        var name = request.Name.Trim();
        var surname = request.Surname.Trim();
        var isExists = await _userRepository.ExistAsync(x => x.Name == name && x.Surname == surname);
        if (isExists)
            return ApiResponse<NoContent>.Fail(400, "User already exists!");
        
        var user = request.Adapt<User>();
        await _userRepository.AddAsync(user);
        return ApiResponse<NoContent>.Success(201);
    }
    
    public async Task<ApiResponse<NoContent>> AddContactInfo(AddContactInfoDto request)
    {
        var isUserExists = await _userRepository.ExistAsync(x => x.UUID == request.UserId);
        if (!isUserExists)
            return ApiResponse<NoContent>.Fail(404, "User not found!");
        
        var value = request.ContactTypeValue.Trim();
        var isExists = await _userContactInfoRepository.ExistAsync(x =>
            x.UserId == request.UserId && x.ContactType == request.ContactType && x.ContactTypeValue == value);
        if (isExists)
            return ApiResponse<NoContent>.Fail(400, "User contact info already exists!");
        
        var contactInfo = request.Adapt<UserContactInfo>();
        await _userContactInfoRepository.AddAsync(contactInfo);
        return ApiResponse<NoContent>.Success(201);
    }

    public async Task<ApiResponse<NoContent>> RemoveContactInfo(AddContactInfoDto request)
    {
        var isUserExists = await _userRepository.ExistAsync(x => x.UUID == request.UserId);
        if (!isUserExists)
            return ApiResponse<NoContent>.Fail(404, "User not found!");
        
        var value = request.ContactTypeValue.Trim();
        var contactInfo = await _userContactInfoRepository.GetAsync(x =>
            x.UserId == request.UserId && x.ContactType == request.ContactType && x.ContactTypeValue == value);
        if (contactInfo == null)
            return ApiResponse<NoContent>.Fail(404, "User contact info not found!");
        
        await _userContactInfoRepository.DeleteAsync(contactInfo);
        return ApiResponse<NoContent>.Success(200);
    }

    public async Task<ApiResponse<NoContent>> RemoveUser(Guid uuid)
    {
        var user = await _userRepository.GetAsync(x => x.UUID == uuid);
        if (user == null)
            return ApiResponse<NoContent>.Fail(404, "User not found!");
        await _userRepository.DeleteAsync(user);
        return ApiResponse<NoContent>.Success(200);
    }
    
    public async Task<ApiResponse<List<UserLocationReport>>> GetReport()
    {
        var report = await _userRepository.GetReport();
        return ApiResponse<List<UserLocationReport>>.Success(200, report);
    }
}