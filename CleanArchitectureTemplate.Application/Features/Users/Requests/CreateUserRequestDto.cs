namespace CleanArchitectureTemplate.Application.Features.Users.Requests
{
    public class CreateUserRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
