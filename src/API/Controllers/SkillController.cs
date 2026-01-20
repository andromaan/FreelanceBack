using API.Controllers.Common;
using BLL.ViewModels.Skill;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SkillController(ISender sender)
    : GenericCrudController<int, SkillVM, CreateSkillVM, UpdateSkillVM>(sender)
{
}