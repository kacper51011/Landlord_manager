using Apartments.API.Configurations;

namespace Apartments.API.Registers
{
    public static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
