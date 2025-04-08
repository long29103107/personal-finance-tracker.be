using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.Domain;

public class IgnorePropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var propertiesToIgnore = new[] { "scopedContext" };

        foreach (var prop in propertiesToIgnore)
        {
            if (schema.Properties.ContainsKey(prop))
            {
                schema.Properties.Remove(prop);
            }
        }
    }
}
