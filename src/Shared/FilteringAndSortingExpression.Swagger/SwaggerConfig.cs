namespace FilteringAndSortingExpression.Swagger;

public class SwaggerConfig
{
    public string Version { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Url
    {
        get
        {
            return $"/swagger/{Version}/swagger.json";
        }
    }

    public string[] Assemblies { get; set; }

    public bool EnableAuthentioncation { get; set; }
}