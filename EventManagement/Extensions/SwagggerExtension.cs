using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Extensions
{
    public static class SwaggerExtension
    {

        public static void UseCustomSwaggerApi(this IApplicationBuilder app, string name = "DAFMIS API V1")
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            //app.UseSwagger();

            app.UseSwagger(c => {
                c.RouteTemplate = "swagger/{documentname}/swagger.json";
            });

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", name);
                c.DocExpansion(DocExpansion.None);
            });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string title = "Dafmis")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = title, Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });
            return services;
        }

        public class SecurityRequirementsDocumentFilter : IDocumentFilter
        {
            public void Apply(SwaggerDocument document, DocumentFilterContext context)
            {
                document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary < string, IEnumerable < string >> ()
                {
                    {"Bearer", new string[] {}},
                    {"Basic", new string[] {}},
                }
            };
            }
        }
    }
}
