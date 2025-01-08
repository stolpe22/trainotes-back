using trainote_api.Context;
using Microsoft.EntityFrameworkCore;
using trainote_api.Models;
using trainote_api.Infrastructure;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;

System.Console.WriteLine("Iniciando a aplicação...");
var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço do DbContext ao contêiner de DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Server=localhost;Port=5432;Database=trainotes;User Id=postgres;Password=123;"));

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5170); // HTTP
    options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
    Console.WriteLine("Kestrel configurado para escutar nas portas 5170 (HTTP) e 5001 (HTTPS)");
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(); // Registra os controladores no contêiner de DI

var app = builder.Build();
System.Console.WriteLine("Aplicação construída, configurando middleware...");

// Configure the HTTP request pipeline.


//app.UseHttpsRedirection(); //comentado para resolver o BO Certificate error: if you trust server provided certificate and not in cert chain, use `@insecure`


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Garante que as rotas dos controladores sejam mapeadas
});


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

ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

app.Use(async (context, next) =>
{
    Console.WriteLine($"Recebida requisição: {context.Request.Method} {context.Request.Path}");
    await next(); Console.WriteLine($"Resposta enviada: {context.Response.StatusCode}");
});

app.Run();
System.Console.WriteLine("Aplicação rodando...");

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
