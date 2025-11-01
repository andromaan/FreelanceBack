using API.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController(ISender sender) : BaseController
{
    
}