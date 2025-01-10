using CrmBackend.Api.Dtos;
using System.Reflection;

using CrmBackend.Api.Services;
using CrmBackend.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;
using CrmBackend.Database;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Utils;

public static class RegisterServicesHelper
{
    public static void RegisterAllServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.RegisterAspServices(configuration);
        services.RegisterDatabaseContext();
        services.RegisterRepositories();
        services.RegisterServices();
    }

    private static void RegisterAspServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var authOptions = new AuthOptions(configuration);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = false,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
        services.AddAuthorization();

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddSignalR();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<UserRepository>();
        services.AddTransient<PasswordRepository>();
        services.AddTransient<AccountRepository>();
        services.AddTransient<ActivityRepository>();
        services.AddTransient<StudentInteractionRepository>();
        services.AddTransient<NotificationRepository>();
        services.AddTransient<CompetenceRepository>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<PasswordHelperService>();
        services.AddTransient<EncryptionService>();
        services.AddTransient<FilterService>();
        //services.AddScoped<NotificationService>();
    }

    private static void RegisterDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql().UseLazyLoadingProxies());
    }
}
