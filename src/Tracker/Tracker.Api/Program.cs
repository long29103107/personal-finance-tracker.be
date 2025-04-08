using Microsoft.EntityFrameworkCore;
using Tracker.Api;
using Tracker.Api.Repositories;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services;
using Tracker.Api.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<FinancialDbContext>(options =>
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("Tracker")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
