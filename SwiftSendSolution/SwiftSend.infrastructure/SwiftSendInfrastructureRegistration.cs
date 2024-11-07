using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SwiftSend.infrastructure.Context;

namespace SwiftSend.infrastructure
{
    public static class SwiftSendInfrastructureRegistration
    {
        public static IServiceCollection RegisterInfrasturcture(this IServiceCollection services,
            IConfiguration configuration, string connectionString,
            string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            // Register MongoDB client and database with DI
            services.AddSingleton<IMongoClient>(client);
            services.AddSingleton(database);

            // Register MongoInitializer for ensuring collections
            services.AddSingleton<MongoInitializer>();

            // Configure Identity services
            services.AddIdentity<MongoUser, IdentityRole>()
                .AddDefaultTokenProviders();

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

            return services;
        }
    }
}
