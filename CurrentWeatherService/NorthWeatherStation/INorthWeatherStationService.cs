using CurrentWeatherService.Shared;

namespace CurrentWeatherService.NorthWeatherStation;

/// <summary>
/// Interface for North Weather Station Service.
/// Provides methods to get weather reports.
/// </summary>
public interface INorthWeatherStationService
{
    /// <summary>
    /// Gets the weather report from the North Weather Station.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of WeatherReportDto.</returns>
    Task<List<WeatherReportDto>> GetWeatherReport();
}
