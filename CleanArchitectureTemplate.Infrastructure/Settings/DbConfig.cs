using CleanArchitectureTemplate.Infrastructure.Persistence.Context;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AuthenticationResult = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult;
using ClientCredential = Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential;

namespace CleanArchitectureTemplate.Infrastructure.Settings
{
    public static class DbConfig
    {
        static string ClientId { get; set; } = string.Empty;
        static string ClientSecret { get; set; } = string.Empty;

        public static IServiceCollection ConfigDbConnection(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            isDevelopment = true;
            if (isDevelopment)
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection")!;
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

                return services;
            }

            ClientId = configuration["KeyVault:ClientId"]!;
            ClientSecret = configuration["KeyVault:ClientSecret"]!;
            string vaultUrl = $"https://{configuration["KeyVault:Vault"]}.vault.azure.net/secrets/{configuration["KeyVault:KeyVaultSecretName"]}/{configuration["KeyVault:Identifier"]}";
            var key = new KeyVaultClient(GetAccessToken, new HttpClient());
            var secret = key.GetSecretAsync(vaultUrl).GetAwaiter().GetResult();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(secret.Value));
            return services;

        }
        private static async Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            var context = new AuthenticationContext(authority, Microsoft.IdentityModel.Clients.ActiveDirectory.TokenCache.DefaultShared);

            ClientCredential credential = new(ClientId, ClientSecret);
            AuthenticationResult result = await context.AcquireTokenAsync(resource, credential);

            if (result == null)
                throw new ArgumentException("Failed to obtain the JWT token in DB connection string configuration.");

            return result.AccessToken;
        }
    }
}
