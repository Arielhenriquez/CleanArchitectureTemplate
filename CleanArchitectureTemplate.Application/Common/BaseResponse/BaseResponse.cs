using CleanArchitectureTemplate.Domain.Constants;
using System.Net;

namespace CleanArchitectureTemplate.Application.Common.BaseResponse
{
    public partial class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public partial class BaseResponse<TEntity> where TEntity : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public TEntity? Data { get; set; }

    }

    public partial class BaseResponse
    {
        public BaseResponse(HttpStatusCode statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static BaseResponse Ok(object data)
        {
            return new BaseResponse(HttpStatusCode.OK, MessageResponse.OkMessage, data);
        }

        public static BaseResponse BadRequest(string errorMessage)
        {
            return new BaseResponse(HttpStatusCode.BadRequest, MessageResponse.ExceptionMessage, errorMessage);
        }

        public static BaseResponse NotFound(string errorMessage)
        {
            return new BaseResponse(HttpStatusCode.NotFound, MessageResponse.NotFoundMessage, errorMessage);
        }

        public static BaseResponse Created(object data)
        {
            return new BaseResponse(HttpStatusCode.Created, MessageResponse.CreatedMessage, data);
        }

        public static BaseResponse Updated(object data)
        {
            return new BaseResponse(HttpStatusCode.NoContent, MessageResponse.UpdatedMessage, data);
        }
        public static BaseResponse Deleted(object data)
        {
            return new BaseResponse(HttpStatusCode.NoContent, MessageResponse.EliminatedMessage, data);
        }

        public static BaseResponse Unauthorized(string errorMessage)
        {
            return new BaseResponse(HttpStatusCode.Unauthorized, MessageResponse.UnauthorizedMessage, errorMessage);
        }

        public static BaseResponse InternalServerError(string errorMessage)
        {
            return new BaseResponse(HttpStatusCode.InternalServerError, MessageResponse.ServerError, errorMessage);
        }

        public static BaseResponse Forbidden(string errorMessage)
        {
            return new BaseResponse(HttpStatusCode.Forbidden, MessageResponse.ForbiddenMessage, errorMessage);
        }
    }
}
