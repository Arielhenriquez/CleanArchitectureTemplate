using CleanArchitectureTemplate.Application.Common.PaginationQuery;
using CleanArchitectureTemplate.Application.Common.PaginationResponse;
using CleanArchitectureTemplate.Application.Features.Users.Requests;
using CleanArchitectureTemplate.Application.Features.Users.Responses;

namespace CleanArchitectureTemplate.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Paged<UserResponseDto>> GetPagedUsers(PaginationQuery paginationQuery, CancellationToken cancellationToken);
        Task<UserResponseDto> GetUserById(Guid id);
        Task<UserResponseDto> CreateUser(CreateUserRequestDto request, CancellationToken cancellationToken = default);
        Task<UserResponseDto> UpdateUser(Guid id, UpdateUserRequestDto request, CancellationToken cancellationToken = default);
        Task<UserResponseDto> DeleteUserById(Guid id);
    }
}
