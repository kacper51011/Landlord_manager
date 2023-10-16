using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Apartments.API.Registers
{
    public static class Versioning
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(cfg =>
            {
                cfg.GroupNameFormat = "'v'VVV";
                cfg.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
    }
}
