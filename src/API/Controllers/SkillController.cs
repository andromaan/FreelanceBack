using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using Domain.Models.Freelance;
using Domain.ViewModels.Skill;
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
        => new GetAll.Query<Skill, int, SkillVM>();

    protected override IRequest<ServiceResponse> GetByIdQuery(int id)
        => new GetById.Query<Skill, int, SkillVM> { Id = id };

    protected override IRequest<ServiceResponse> CreateCommand(CreateSkillVM vm)
        => new Create.Command<CreateSkillVM, Skill, int> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateSkillVM vm)
        => new Update.Command<UpdateSkillVM, Skill, int> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new Delete.Command<Skill, int> { Id = id };
}