using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.IdentityModel.Tokens;

using FilteringAndSortingExpression.Swagger.Extensions;
using Shared.Dtos.Extensions;

namespace FilteringAndSortingExpression.Swagger.Helpers;

public static class Helper
{
    internal static string[] IgnoreProperties = new string[] { "scopedContext" };

    public class SwaggerExcludeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.IsNullOrEmpty())
                return;

            foreach(var item in operation.Parameters)
            {
                item.Name = item.Name.LowerFirstChar();
            }

            foreach (var property in Helper.IgnoreProperties)
            {
                operation.Parameters = operation.Parameters.Where(p => !p.Name.StartsWith(property)).ToList();
            }
        }
    }

    public class SwaggerIgnoreFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var excludeProperties = context.Type?.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SwaggerIgnoreAttribute))).ToList();

            if (excludeProperties.IsNullOrEmpty())
                return;
            
            foreach (var property in excludeProperties)
            {
                var propertyName = property.Name.LowerFirstChar();

                if (schema.Properties.ContainsKey(propertyName))
                {
                    schema.Properties.Remove(propertyName);
                }
            }
        }
    }


}