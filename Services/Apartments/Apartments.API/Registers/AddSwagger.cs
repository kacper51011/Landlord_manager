using Apartments.API.Configurations;
using System.Reflection;

namespace Apartments.API.Registers
{
    public static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            services.AddSwaggerGen((setup)=>
            {
                setup.IncludeXmlComments(xmlCommentPath);
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
