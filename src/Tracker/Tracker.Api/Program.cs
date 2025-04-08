using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBlog.Shared.Autofac.Modules;
using Tracker.Api;
using Shared.Domain;
using FluentValidation.AspNetCore;
using Shared.ExceptionHandler;
using Shared.Serilog;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<FinancialDbContext>(options =>
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("Tracker")));
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddSerilogMiddleware();

Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .Enrich.WithExceptionDetails()
           .Enrich.WithMachineName()
           .WriteTo.Console()
           .CreateLogger();

builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));
builder.Host.ConfigureLogging(HostBuilderExtensions.ConfigureLogging);

builder.Services.AddFluentValidation(v =>
{
    v.ImplicitlyValidateChildProperties = true;
    v.ImplicitlyValidateRootCollectionElements = true;
    v.RegisterValidatorsFromAssembly(TrackerApiReference.Assembly);
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureContainer<ContainerBuilder>(container =>
          {
              container.RegisterModule(new GeneralModule(
                  TrackerApiReference.Assembly,
                   TrackerApiReference.Assembly)
              );
          });

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<IgnorePropertiesSchemaFilter>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Enable JWT Bearer Authentication in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {your token}' in the field below. Example: 'Bearer abc123'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
