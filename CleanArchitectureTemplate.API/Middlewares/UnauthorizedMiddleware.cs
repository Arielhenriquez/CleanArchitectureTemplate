using CleanArchitectureTemplate.Application.Common.BaseResponse;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchitectureTemplate.API.Middlewares;
public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            var errorMessage = "Please use the login endpoint for authentication";
            var unauthorizedResponse = BaseResponse.Unauthorized(errorMessage);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(unauthorizedResponse));
        }
    }
}
