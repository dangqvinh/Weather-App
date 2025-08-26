using Weather.Api.Endpoints;
using Weather.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.UseAppMiddlewares();

// app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.MapWeatherEndpoints();

app.Run();

