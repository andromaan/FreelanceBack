using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using BLL.ViewModels.Skill;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SkillController(ISender sender, IMapper mapper)
    : CrudControllerBase<int, CreateSkillVM, UpdateSkillVM>(sender, mapper)
{
    protected override IRequest<ServiceResponse> GetAllQuery()
        => new GetAll.Query<SkillVM>();

    protected override IRequest<ServiceResponse> GetByIdQuery(int id)
        => new GetById.Query<int, SkillVM> { Id = id };

    protected override IRequest<ServiceResponse> CreateCommand(CreateSkillVM vm)
        => new Create.Command<CreateSkillVM> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateSkillVM vm)
        => new Update.Command<UpdateSkillVM, int> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new Delete.Command<SkillVM, int> { Id = id };
}