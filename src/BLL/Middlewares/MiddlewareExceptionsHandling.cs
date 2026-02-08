using System.Net;
using BLL.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Middlewares;

public class MiddlewareExceptionsHandling(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (SecurityTokenException ex)
        {
            await context.Response.WriteJsonResponseAsync(StatusCodes.Status426UpgradeRequired,
                ServiceResponse.GetResponse(ex.Message, false, null, HttpStatusCode.UpgradeRequired));
        }
        catch (ValidationException ex)
        {
            await context.Response.WriteJsonResponseAsync(StatusCodes.Status400BadRequest,
                ServiceResponse.BadRequest(ex.Message ?? throw new ArgumentNullException(nameof(ex))));
        }
        catch (Exception ex)
        {
            await context.Response.WriteJsonResponseAsync(StatusCodes.Status500InternalServerError,
                ServiceResponse.InternalError(ex.Message));
        }
    }
}