using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SwiftSend.infrastructure.Options.JwtOpts
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>

    {
        public const string Section = "JwtOptions";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(Section)
                .Bind(options);
        }
    }
}
