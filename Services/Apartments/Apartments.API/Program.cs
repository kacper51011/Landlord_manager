using Apartments.API.Configurations;
using Apartments.API.Registers;
using Apartments.Application.Commands.CreateOrUpdateApartment;
using Apartments.Domain.Interfaces;
using Apartments.Infrastructure.Db;
using Apartments.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureOptions<MongoSettingsSetup>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateOrUpdateApartmentCommandHandler)));

builder.Services.AddSingleton<IApartmentsRepository, ApartmentsRepository>();

builder.Services.AddControllers();

builder.Services.AddVersioning(builder.Configuration);

builder.Services.AddSwagger(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        { 
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
