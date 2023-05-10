using CleanArchitectureTemplate.Application.Features.Users.Requests;
using FluentValidation;

namespace CleanArchitectureTemplate.Application.Features.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
