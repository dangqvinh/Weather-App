using Weather.Api.Endpoints;
using Weather.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseAppMiddlewares();

// app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.UseCors("web");

app.MapMapEndpoints();
app.MapWeatherEndpoints();

app.Run();

