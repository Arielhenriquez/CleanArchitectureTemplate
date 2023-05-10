using Response = CleanArchitectureTemplate.Application.Common.BaseResponse.BaseResponse;

namespace CleanArchitectureTemplate.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public Response Response { get; }

        public BadRequestException(string message)
            : base(message)
        {
            Response = Response.BadRequest(message);
        }
    }
}
