using CurrentWeatherService.Shared;

namespace CurrentWeatherService.EastWeatherStation;

/// <summary>
/// Interface for East Weather Station Service.
/// Provides methods to get weather reports.
/// </summary>
public interface IEastWeatherStationService
{
    /// <summary>
    /// Gets the weather report from the East Weather Station.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of WeatherReportDto.</returns>
    Task<List<WeatherReportDto>> GetWeatherReport();
}
