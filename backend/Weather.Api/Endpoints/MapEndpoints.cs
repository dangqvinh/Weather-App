using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Weather.Api.Endpoints
{
    public static class MapEndpoints
    {
        public static void MapMapEndpoints(this WebApplication app)
        {
            app.MapGet("/api/map-style", async (IHttpClientFactory httpClientFactory) =>
            {
                var client = httpClientFactory.CreateClient();

                var trackAsiaStyleUrl =
                    "https://maps.track-asia.com/styles/v2/streets.json?key=406172ef1756263298fe7c6b66f9695409";

                try
                {
                    var response = await client.GetAsync(trackAsiaStyleUrl);

                    response.EnsureSuccessStatusCode(); // ném exception nếu status != 2xx

                    var content = await response.Content.ReadAsStringAsync();

                    return Results.Content(content, "application/json");
                }
                catch (HttpRequestException ex)
                {
                    return Results.Problem("Failed to fetch TrackAsia style JSON: " + ex.Message);
                }
            })
            .WithName("GetMapStyle")
            .WithTags("Map");
        }
    }
}
