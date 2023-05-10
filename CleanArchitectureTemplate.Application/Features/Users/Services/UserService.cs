using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureTemplate.Application.Common.Exceptions;
using CleanArchitectureTemplate.Application.Common.Extensions;
using CleanArchitectureTemplate.Application.Common.PaginationQuery;
using CleanArchitectureTemplate.Application.Common.PaginationResponse;
using CleanArchitectureTemplate.Application.Features.Users.Requests;
using CleanArchitectureTemplate.Application.Features.Users.Responses;
using CleanArchitectureTemplate.Application.Interfaces;
using CleanArchitectureTemplate.Application.Interfaces.Services;
using UsersEntity = CleanArchitectureTemplate.Domain.Entities.Users;

namespace CleanArchitectureTemplate.Application.Features.Users.Services;
public class UserService : IUserService
{
    private readonly IBaseRepository<UsersEntity> _userRepository;
    private readonly IMapper _mapper;
    public UserService(IBaseRepository<UsersEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task<Paged<UserResponseDto>> GetPagedUsers(PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _userRepository.Query().OrderByDescending(c => c.CreatedDate);

        var queryMapped = query
         .ProjectTo<UserResponseDto>(_mapper.ConfigurationProvider);

        var paginatedResult = queryMapped
        .Paginate(paginationQuery.PageNumber, paginationQuery.PageSize, cancellationToken);

        return paginatedResult;
    }

    public async Task<UserResponseDto> GetUserById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        var dto = _mapper.Map<UserResponseDto>(user);
        return dto;
    }

    public async Task<UserResponseDto> CreateUser(CreateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        bool userExists = _userRepository.Query().Any(x => x.UserName == request.UserName);
        if (userExists) throw new BadRequestException($"El nombre de usuario: {request.UserName} ya existe");

        var userEntity = _mapper.Map<UsersEntity>(request);
        var user = await _userRepository.AddAsync(userEntity, cancellationToken);
        var dto = _mapper.Map<UserResponseDto>(user);

        return dto;
    }

    public async Task<UserResponseDto> UpdateUser(UpdateUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<UsersEntity>(request);
        var result = await _userRepository.UpdateAsync(user);

        var dto = _mapper.Map<UserResponseDto>(result);
        return dto;
    }

    public async Task<UserResponseDto> DeleteUserById(Guid id)
    {
        var result = await _userRepository.Delete(id);

        var dto = _mapper.Map<UserResponseDto>(result);

        return dto;
    }
}
