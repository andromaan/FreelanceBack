using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using Domain.Models.Languages;
using Domain.ViewModels.Language;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LanguageController(ISender sender, IMapper mapper)
    : CrudControllerBase<int, CreateLanguageVM, UpdateLanguageVM>(sender, mapper)
{
    protected override IRequest<ServiceResponse> GetAllQuery()
        => new GetAll.Query<Language, int, LanguageVM>();
    
    protected override IRequest<ServiceResponse> GetByIdQuery(int id) 
        => new GetById.Query<Language, int, LanguageVM> { Id = id };
    
    protected override IRequest<ServiceResponse> CreateCommand(CreateLanguageVM vm)
        => new Create.Command<CreateLanguageVM, Language, int> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateLanguageVM vm)
        => new Update.Command<UpdateLanguageVM, Language, int> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new Delete.Command<Language, int> { Id = id };
}