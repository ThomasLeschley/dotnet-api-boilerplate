using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Boilerplate.Api.Infrastructure.Extensions{

    public static class SwaggerExtensions{
        /// <summary>
        /// Authentication will be set up to use jwt bearer tokens
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services){
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Boilerplate API",
                    Version = "v1"
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securitySchema,
                        new string[] {"Bearer"}
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });

            return services;
        }

        /// <summary>
        /// UI is served at /ui/swagger <br/>
        /// OpenAPI definition is served at /swagger/v1/swagger.json or /swagger/v1/swagger.yaml
        /// </summary>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate V1 API");
                c.RoutePrefix = "ui/swagger";
            });

            return app;
        }
    }
}