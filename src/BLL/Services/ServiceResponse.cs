using System.Net;

namespace BLL.Services;

public class ServiceResponse
{
    public required string Message { get; set; }
    public bool Success { get; set; }
    public object? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ServiceResponse GetResponse(string message, bool success, object? data, HttpStatusCode statusCode)
    {
        return new ServiceResponse
        {
            Message = message,
            Success = success,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static ServiceResponse Ok(string message = "Ok", object? data = null)
    {
        return GetResponse(message, true, data, HttpStatusCode.OK);
    }

    public static ServiceResponse BadRequest(string message, object? data = null)
    {
        return GetResponse(message, false, data, HttpStatusCode.BadRequest);
    }

    public static ServiceResponse InternalError(string message, object? data = null)
    {
        return GetResponse(message, false, data, HttpStatusCode.InternalServerError);
    }
        
    public static ServiceResponse NotFound(string message, object? data = null)
    {
        return GetResponse(message, false, data, HttpStatusCode.NotFound);
    }

    public static ServiceResponse Forbidden(string message, object? data = null)
    {
        return GetResponse(message, false, data, HttpStatusCode.Forbidden);
    }
    
    public static ServiceResponse Unauthorized(string message, object? data = null)
    {
        return GetResponse(message, false, data, HttpStatusCode.Unauthorized);
    }
}