using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("LonG",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseCors("LonG");

app.UseRouting();

app.UseAuthorization();

app.UseSwaggerForOcelotUI();

app.UseOcelot()
    .Wait();

app.Run();