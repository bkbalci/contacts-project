using ContactProject.Core.BaseControllers;
using ContactService.Application.Services;
using ContactService.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : BaseController
{
    private readonly UserService _userService;

    public ContactController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AddContactInfoDto request)
    {
        var response = await _userService.AddContactInfo(request);
        return CreateActionResult(response);
    }
    
    
    [HttpDelete]
    public async Task<IActionResult> Delete(AddContactInfoDto request)
    {
        var response = await _userService.RemoveContactInfo(request);
        return CreateActionResult(response);
    }
}