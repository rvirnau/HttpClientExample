namespace CurrentWeatherService.Shared;

/// <summary>
/// Provides extension methods for adding endpoints to the IEndpointRouteBuilder.
/// </summary>
public static class Endpoints
{
    /// <summary>
    /// Adds the weather forecast endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to add the endpoints to.</param>
    public static void AddEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/eastweatherstation", Handlers.GetEastWeatherForecast)
            .WithName("eastweatherstation")
            .WithTags("Weather")
            .WithMetadata(new { Description = "Get the weather forecast from the east weather station" })
            .WithOpenApi();

        endpoints.MapGet("/westweatherstation", Handlers.GetWestWeatherForecast)
            .WithName("westweatherstation")
            .WithTags("Weather")
            .WithMetadata(new { Description = "Get the weather forecast from the west weather station" })
            .WithOpenApi();

        endpoints.MapGet("/northweatherstation", Handlers.GetNorthWeatherForecast)
            .WithName("northweatherstation")
            .WithTags("Weather")
            .WithMetadata(new { Description = "Get the weather forecast from the north weather station" })
            .WithOpenApi();
    }
}
