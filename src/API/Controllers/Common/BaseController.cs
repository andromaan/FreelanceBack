using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common;

public class BaseController : ControllerBase
{
    protected IActionResult GetResult(ServiceResponse serviceResponse)
    {
        return StatusCode((int)serviceResponse.StatusCode, serviceResponse);
    }
}