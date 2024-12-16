using System.Text.Json;using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using WhatsForDinner.DataAccess.Postgres;
using WhatsForDinner.Endpoints;
using WhatsForDinner.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .MinimumLevel.Information()
        .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
        .Enrich.FromLogContext();
});

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IDishWriterService, DishWriterService>();
builder.Services.AddScoped<IDishReaderService, DishReaderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<WfdDbContext>(options =>
{
    options
        .UseNpgsql(configuration.GetConnectionString(nameof(WfdDbContext)))
        .EnableSensitiveDataLogging();
});

var app = builder.Build();


app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");

//app.UseAuthentication();
//app.UseAuthorization();
app.MapDishesEndpoints();

app.Run();