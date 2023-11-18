using ContactProject.Core.BaseControllers;
using ContactService.Application.Services;
using ContactService.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _userService.GetUsers();
        return CreateActionResult(response);
    }
    
    [HttpGet("{uuid}")]
    public async Task<IActionResult> Get(Guid uuid)
    {
        var response = await _userService.GetUserById(uuid);
        return CreateActionResult(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto request)
    {
        var response = await _userService.CreateUser(request);
        return CreateActionResult(response);
    }
    
    [HttpDelete("{uuid}")]
    public async Task<IActionResult> Delete(Guid uuid)
    {
        var response = await _userService.RemoveUser(uuid);
        return CreateActionResult(response);
    }
}