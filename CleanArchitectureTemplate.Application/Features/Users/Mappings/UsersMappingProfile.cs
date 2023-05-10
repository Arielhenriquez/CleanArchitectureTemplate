using AutoMapper;
using CleanArchitectureTemplate.Application.Features.Users.Requests;
using CleanArchitectureTemplate.Application.Features.Users.Responses;
using UsersEntity = CleanArchitectureTemplate.Domain.Entities.Users;

namespace CleanArchitectureTemplate.Application.Features.Users.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UsersEntity, UserResponseDto>();
            //CreateMap<UpdateUserRequestDto, UsersEntity>();
            CreateMap<CreateUserRequestDto, UsersEntity>();
        }
    }
}
