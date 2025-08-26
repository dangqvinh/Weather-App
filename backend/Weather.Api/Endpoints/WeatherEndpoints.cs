using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Weather.Api.Models;

namespace Weather.Api.Endpoints;

public static class WeatherEndpoints
{
    public static void MapWeatherEndpoints(this WebApplication app)
    {
        app.MapGet("/health", () => Results.Ok(new { ok = true }));

        app.MapGet("/api/weather", async (
            [FromQuery] double lat,
            [FromQuery] double lon,
            IMemoryCache cache,
            IHttpClientFactory httpClientFactory
        ) =>
        {
            var cacheKey = $"openmeteo:{lat:F4},{lon:F4}";
            if (cache.TryGetValue(cacheKey, out var cachedData))
                return Results.Ok(cachedData);

            var client = httpClientFactory.CreateClient("OpenMeteo");

            // gọi API Open-Meteo: current weather
            var url = $"/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

                var res = await client.GetFromJsonAsync<OpenMeteoResponse>(url);

            if (res is null || res.CurrentWeather is null)
                return Results.Problem("Failed to fetch weather data from Open-Meteo API");


            cache.Set(cacheKey, res, TimeSpan.FromMinutes(5));

            return Results.Ok(res);
        });
    }
}
