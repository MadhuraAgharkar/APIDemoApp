using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemoApp
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(o => o.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
        public class ReplaceVersionWithExactValue : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = swaggerDoc.Paths;
                swaggerDoc.Paths = new OpenApiPaths();
                foreach (var path in paths)
                {
                    var key = path.Key.Replace("v{version}", swaggerDoc.Info.Version);
                    var value = path.Value;
                    swaggerDoc.Paths.Add(key, value);
                }
                  
            }
        }
    }
}
