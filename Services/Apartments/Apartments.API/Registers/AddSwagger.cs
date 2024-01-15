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
            services.AddSwaggerGen((setup) =>
            {

                setup.IncludeXmlComments(xmlCommentPath);

                setup.SwaggerDoc("ApartmentsApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Apartments Api",
                    Version = "1.0",
                    Description = "Apartments service created for Landlords project, provide simple interface for managing apartments",
                    Contact = new()
                    {
                        Email = "kacper.tylec1999@gmail.com",
                        Name = "Kacper Tylec",
                    }
                    

                });
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
