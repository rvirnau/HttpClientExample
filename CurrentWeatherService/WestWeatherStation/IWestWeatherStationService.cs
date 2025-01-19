using CurrentWeatherService.Shared;

namespace CurrentWeatherService.WestWeatherStation;

/// <summary>
/// Interface for the West Weather Station Service.
/// </summary>
public interface IWestWeatherStationService
{
    /// <summary>
    /// Retrieves the weather report from the West Weather Station.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of WeatherReportDto.</returns>
    Task<List<WeatherReportDto>> GetWeatherReport();
}
