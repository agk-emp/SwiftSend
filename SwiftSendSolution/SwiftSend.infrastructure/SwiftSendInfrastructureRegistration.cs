using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SwiftSend.app.Abstracts.Repositories;
using SwiftSend.app.Abstracts.Services;
using SwiftSend.data.Entities.Identity;
using SwiftSend.infrastructure.Context;
using SwiftSend.infrastructure.Options.DatabaseOpts;
using SwiftSend.infrastructure.Options.JwtOpts;
using SwiftSend.infrastructure.Repositories;
using SwiftSend.infrastructure.Services;
using System.Text;

namespace SwiftSend.infrastructure
{
    public static class SwiftSendInfrastructureRegistration
    {
        public static IServiceCollection RegisterInfrasturcture(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<DatabaseOptionsSetup>();
            var provider = services.BuildServiceProvider();
            var jwtOptions = provider.GetRequiredService<IOptions<JwtOptions>>().Value;
            var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            var client = new MongoClient(databaseOptions.ConnectionString);
            var database = client.GetDatabase(databaseOptions.DatabaseName);
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            // Register MongoDB client and database with DI
            services.AddSingleton<IMongoClient>(client);
            services.AddSingleton(database);

            // Register MongoInitializer for ensuring collections
            services.AddSingleton<AppDbContext>();
            services.AddScoped<AppDbContext>();

            ConfigureJwt(services, jwtOptions);

            // Configure Identity options
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<AppUser, AppRole>()
        .AddMongoDbStores<AppUser, AppRole, string>(
           databaseOptions.ConnectionString, databaseOptions.DatabaseName)
        .AddDefaultTokenProviders();

            InverseResponsibility(services);

            return services;
        }

        private static void InverseResponsibility(IServiceCollection services)
        {
            services.AddTransient<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IRestaurantServices, RestaurantServices>();
        }

        private static void ConfigureJwt(IServiceCollection services,
            JwtOptions jwtOptions)
        {


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = tokenValidationParameters;
         });
        }
    }
}
