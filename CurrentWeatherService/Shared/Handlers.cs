using CurrentWeatherService.WestWeatherStation;
using CurrentWeatherService.EastWeatherStation;
using CurrentWeatherService.NorthWeatherStation;

namespace CurrentWeatherService.Shared;

/// <summary>
/// Provides handler methods to get weather forecasts from different weather stations.
/// </summary>
public static class Handlers
{
    /// <summary>
    /// Gets the weather forecast from the West Weather Station.
    /// </summary>
    /// <param name="westWeatherStationService">The service to get the weather report from the West Weather Station.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IResult with the weather forecast.</returns>
    public static async Task<IResult> GetWestWeatherForecast(IWestWeatherStationService westWeatherStationService)
    {
        var forecast = await westWeatherStationService.GetWeatherReport();
        return Results.Ok(forecast);
    }

    /// <summary>
    /// Gets the weather forecast from the East Weather Station.
    /// </summary>
    /// <param name="eastWeatherStationService">The service to get the weather report from the East Weather Station.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IResult with the weather forecast.</returns>
    public static async Task<IResult> GetEastWeatherForecast(IEastWeatherStationService eastWeatherStationService)
    {
        var forecast = await eastWeatherStationService.GetWeatherReport();
        return Results.Ok(forecast);
    }

    /// <summary>
    /// Gets the weather forecast from the North Weather Station.
    /// </summary>
    /// <param name="northWeatherStationService">The service to get the weather report from the North Weather Station.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an IResult with the weather forecast.</returns>
    public static async Task<IResult> GetNorthWeatherForecast(INorthWeatherStationService northWeatherStationService)
    {
        var forecast = await northWeatherStationService.GetWeatherReport();
        return Results.Ok(forecast);
    }
}
