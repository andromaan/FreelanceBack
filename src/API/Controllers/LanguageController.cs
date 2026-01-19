using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using BLL.ViewModels.Language;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// [Authorize(Roles = Settings.Roles.AdminRole)]
public class LanguageController(ISender sender)
    : GenericCrudController<int, LanguageVM, CreateLanguageVM, UpdateLanguageVM>(sender)
{
}