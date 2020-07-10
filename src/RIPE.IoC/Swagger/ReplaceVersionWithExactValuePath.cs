using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RIPE.IoC.Swagger
{
    class ReplaceVersionWithExactValuePath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var path = new OpenApiPaths();

            foreach (var paths in swaggerDoc.Paths)
            {
                path.Add(paths.Key.Replace("v{version}", swaggerDoc.Info.Version), paths.Value);
            }
            swaggerDoc.Paths = path;
        }
    }
}
