using CleanArchitectureTemplate.API.Filters;
using CleanArchitectureTemplate.API.Middlewares;
using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Domain.Settings;
using CleanArchitectureTemplate.Infrastructure;
using CleanArchitectureTemplate.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.API.Settings
{
    public class AppSetup
    {
        public AppSetup(ConfigurationManager configuration)
        {
            Configuration = configuration;
        }
        public ConfigurationManager Configuration { get; set; }
        public void Configure(IWebHostEnvironment env)
        {
            Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                optional: false, reloadOnChange: true);
        }
        public void RegisterServices(IServiceCollection services)
        {
            bool isDevelopment = Convert.ToBoolean(Environment.GetEnvironmentVariable("IS_DEVELOPMENT"));
            services.ConfigDbConnection(Configuration, isDevelopment);

            services.Configure<AzureAdClientSettings>(Configuration.GetSection("AzureAdClientSettings"));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            ;
            //services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddHttpContextAccessor();
            services.AddInfrastructure();
            services.AddApplication();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WALDO.TEMPLATE", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        builder.SetIsOriginAllowed(x => true);
                    });
            });
        }
        public void SetupMiddlewares(WebApplication app)
        {
            app.UseCors("DevPolicy");
            app.UseMiddleware<UnauthorizedMiddleware>();

        }
    }
}
