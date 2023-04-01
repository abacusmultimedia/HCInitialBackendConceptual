using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DAO.PlanningPortal.Api.Common.Extension;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerUI(this IServiceCollection services, string description)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = description, Version = "v1" });
            //c.IncludeXmlComments("SwaggerSetupExample.xml");
            // To Enable authorization using Swagger (JWT)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
            });
        });

        return services;
    }
}