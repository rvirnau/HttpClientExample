using CurrentWeatherService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CurrentWeatherService.WestWeatherStation;

/// <summary>
/// Service to interact with the West Weather Station API.
/// </summary>
public class WestWeatherStationService : IWestWeatherStationService
{
    private readonly IWestWeatherStationHttpClient _westWeatherStationClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="WestWeatherStationService"/> class.
    /// </summary>
    /// <param name="westWeatherStationClient">The HTTP client to interact with the West Weather Station API.</param>
    public WestWeatherStationService(IWestWeatherStationHttpClient westWeatherStationClient)
    {
        _westWeatherStationClient = westWeatherStationClient;
    }

    /// <summary>
    /// Gets the weather report from the West Weather Station.
    /// </summary>
    /// <returns>A list of <see cref="WeatherReportDto"/> containing the weather report.</returns>
    /// <response code="200">Returns the weather report.</response>
    /// <response code="500">If there is an error in the response.</response>
    /// <response code="404">If the weather report is not found.</response>
    /// <response code="400">If the request is invalid.</response>
    [HttpGet("westweatherstation")]
    [ProducesResponseType(typeof(List<WeatherReportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<WeatherReportDto>> GetWeatherReport()
    {
        var responce = await _westWeatherStationClient.GetAsync("westweatherstation");

        try { responce.EnsureSuccessStatusCode(); }
        catch (Exception e)
        {
            throw new Exception("Error in the response", e);
        }

        try
        {
            var stringContent = await responce.Content.ReadAsStringAsync();
            var report = JsonSerializer.Deserialize<List<WeatherReportDto>>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return report;
        }
        catch (Exception e)
        {
            throw new Exception("Error parsing responce", e);
        }
    }
}
