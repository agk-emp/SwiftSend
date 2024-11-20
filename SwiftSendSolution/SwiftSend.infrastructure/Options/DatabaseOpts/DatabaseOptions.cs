namespace SwiftSend.infrastructure.Options.DatabaseOpts
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AppUsers { get; set; }
        public string UserRefreshTokens { get; set; }
        public string Restaurants { get; set; }
    }
}
