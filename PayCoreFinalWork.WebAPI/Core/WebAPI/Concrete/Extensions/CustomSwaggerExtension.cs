using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace PayCoreFinalWork.Core.WebAPI.Concrete.Extensions
{
    public static class CustomSwaggerExtension
    {
        // Swagger üzerinde JWT Bearer konfigürasyonu için gerekli extension metod.
        public static IServiceCollection AddCustomizeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayCore - Final Project API Management", Version = "v1.0" });
                c.OperationFilter<SwaggerFileOperationFilter>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "API Management for PayCore - Final Project",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            return services;
        }
    }
}