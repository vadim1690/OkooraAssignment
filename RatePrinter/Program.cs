using Domain.Data.Configuration;
using Domain.Data.Repositories.RatePairRepository;
using Microsoft.OpenApi.Models;
using RatePrinter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
        .InitializeDb()
        .EnsureDatabase();

// Register services
builder.Services.AddScoped<IRatePairRepository, RatePairRepository>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Exchange Rates API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Rates API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();