using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace RIPE.IoC
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "RIPE.API.xml");
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}