using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.Features.Users.Requests
{
    public class UpdateUserRequestDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
