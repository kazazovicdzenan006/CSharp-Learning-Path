/*using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerConfiguration
{
    public static void AddSwaggerAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCity API", Version = "v1" });

            // 1. Definisanje sheme
            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Unesite token (npr. 'bearer {token}')",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            opt.AddSecurityDefinition("Bearer", scheme);

            // 2. Dodavanje zahtjeva (Ovdje je bila greška CS1950/CS1503)
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { scheme, new string[] { } }
            });
        });
    }
}
*/