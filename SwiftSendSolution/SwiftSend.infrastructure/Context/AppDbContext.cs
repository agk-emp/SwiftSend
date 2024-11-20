using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SwiftSend.data.Entities;
using SwiftSend.data.Entities.Identity;
using SwiftSend.infrastructure.Options.DatabaseOpts;

namespace SwiftSend.infrastructure.Context
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly DatabaseOptions _databaseOptions;

        public AppDbContext(IConfiguration configuration,
             IOptions<DatabaseOptions> databaseOptions)
        {

            _databaseOptions = databaseOptions.Value;
            var client = new MongoClient(_databaseOptions.ConnectionString);
            _database = client.GetDatabase(_databaseOptions.DatabaseName);
        }

        public IMongoCollection<AppUser> Users => _database.GetCollection<AppUser>(_databaseOptions.AppUsers);
        public IMongoCollection<UserRefreshToken> UserRefreshTokens => _database.GetCollection<UserRefreshToken>(_databaseOptions.UserRefreshTokens);
        public IMongoCollection<Restaurant> Restaurants => _database.GetCollection<Restaurant>(_databaseOptions.Restaurants);
    }
}
