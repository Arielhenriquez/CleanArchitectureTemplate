using CleanArchitectureTemplate.Application.Common.BaseResponse;
using CleanArchitectureTemplate.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CleanArchitectureTemplate.API.Filters;
public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            NotFoundException notFoundException => HandleNotFoundException(notFoundException),
            BadRequestException badRequestException => HandleBadRequestException(badRequestException),
            _ => HandleUnhandledException(context.Exception)
        };

        context.ExceptionHandled = true;
    }

    private static IActionResult HandleNotFoundException(NotFoundException notFoundException)
    {
        return new ObjectResult(notFoundException.Response)
        {
            StatusCode = (int)notFoundException.Response.StatusCode
        };
    }

    private static IActionResult HandleBadRequestException(BadRequestException badRequestException)
    {
        return new ObjectResult(badRequestException.Response)
        {
            StatusCode = (int)badRequestException.Response.StatusCode
        };
    }

    private static IActionResult HandleUnhandledException(Exception exception)
    {
        var errorMessage = "An unexpected error occurred. Please try again later.";
        var errorResponse = BaseResponse.InternalServerError(exception.Message);

        // Log the exception details on the server
        // ...

        return new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

}


