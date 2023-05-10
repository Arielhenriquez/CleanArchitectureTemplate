using CleanArchitectureTemplate.Application.Interfaces;
using CleanArchitectureTemplate.Application.Interfaces.Providers;
using CleanArchitectureTemplate.Infrastructure.Persistence.Context;
using CleanArchitectureTemplate.Infrastructure.Persistence.Repositories;
using CleanArchitectureTemplate.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IDbContext, ApplicationDbContext>();
            services.AddTransient<IAzureADUserProvider, AzureADUserProvider>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
