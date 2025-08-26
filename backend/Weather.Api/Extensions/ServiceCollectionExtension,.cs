using Microsoft.OpenApi.Models;

namespace Weather.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("web", p => p
            .WithOrigins(configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
        });

        services.AddMemoryCache();

        // services.AddHttpClient("OpenWeather", client =>
        // {
        //     client.BaseAddress = new Uri(configuration["OpenWeatherMap:BaseUrl"]!);
        // });

        services.AddHttpClient("OpenMeteo", client =>
        {
            client.BaseAddress = new Uri("https://api.open-meteo.com");
        });

        return services;
    }
}