using Apartments.Infrastructure.Db;
using Microsoft.Extensions.Options;

namespace Apartments.API.Configurations
{
    public class MongoSettingsSetup: IConfigureOptions<MongoSettings>
    {
        private const string Section = "MongoDB";
        private readonly IConfiguration _configuration;

        public MongoSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(MongoSettings options)
        {
            _configuration.GetSection(Section).Bind(options);
        }
    }
}
