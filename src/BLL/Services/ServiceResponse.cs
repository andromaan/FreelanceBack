using System.Net;

namespace BLL.Services;

public class ServiceResponse
{
    public required string Message { get; set; }
    public bool Success { get; set; }
    public object? Payload { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ServiceResponse GetResponse(string message, bool success, object? payload, HttpStatusCode statusCode)
    {
        return new ServiceResponse
        {
            Message = message,
            Success = success,
            Payload = payload,
            StatusCode = statusCode
        };
    }

    public static ServiceResponse Ok(string message, object? payload = null)
    {
        return GetResponse(message, true, payload, HttpStatusCode.OK);
    }

    public static ServiceResponse BadRequest(string message, object? payload = null)
    {
        return GetResponse(message, false, payload, HttpStatusCode.BadRequest);
    }

    public static ServiceResponse InternalError(string message, object? payload = null)
    {
        return GetResponse(message, false, payload, HttpStatusCode.InternalServerError);
    }
        
    public static ServiceResponse NotFound(string message, object? payload = null)
    {
        return GetResponse(message, false, payload, HttpStatusCode.NotFound);
    }

    public static ServiceResponse Forbidden(string message, object? payload = null)
    {
        return GetResponse(message, false, payload, HttpStatusCode.Forbidden);
    }
}