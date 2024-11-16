using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SwiftSend.infrastructure.Options.DatabaseOpts
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        public const string Section = "MongoDB";
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            _configuration.GetSection(Section)
                .Bind(options);
        }
    }
}
