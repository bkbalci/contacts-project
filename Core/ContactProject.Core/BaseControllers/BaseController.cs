using ContactService.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ContactProject.Core.BaseControllers;

public class BaseController
{
    public IActionResult CreateActionResult<T>(ApiResponse<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response != null ? response.StatusCode : 404,
        };
    }
}