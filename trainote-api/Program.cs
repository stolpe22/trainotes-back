using trainote_api.Context;
using Microsoft.EntityFrameworkCore;
using trainote_api.Models;
using trainote_api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço do DbContext ao contêiner de DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Server=localhost;Port=5432;Database=trainotes;User Id=postgres;Password=123;"));

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5170); // HTTP
    options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
