namespace CleanArchitectureTemplate.Domain.Settings
{
    public class AzureAdClientSettings
    {
        public string? ServicePrincipalsId { get; set; }
        public string? AdminAppId { get; set; }
        public string? ClientId { get; set; }
        public string? TenantId { get; set; }
        public string? ClientSecret { get; set; }
        public string? GraphUri { get; set; }
        public string? TokenUrl { get; set; }
        public string? AuthorizationUrl { get; set; }
        public string? RedirectUri { get; set; }
        public string? Scopes { get; set; }
    }
}
