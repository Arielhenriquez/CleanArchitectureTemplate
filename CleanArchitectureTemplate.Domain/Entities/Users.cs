namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Users : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
