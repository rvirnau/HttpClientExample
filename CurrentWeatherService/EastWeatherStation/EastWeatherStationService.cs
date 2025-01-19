using CurrentWeatherService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CurrentWeatherService.EastWeatherStation;

/// <summary>
/// Service to interact with the East Weather Station API.
/// </summary>
public class EastWeatherStationService : IEastWeatherStationService
{
    private readonly HttpClient _eastWeatherStationClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="EastWeatherStationService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The factory to create HttpClient instances.</param>
    public EastWeatherStationService(IHttpClientFactory httpClientFactory)
    {
        _eastWeatherStationClient = httpClientFactory.CreateClient("eastWeatherStationHttpClient");
    }

    /// <summary>
    /// Gets the weather report from the East Weather Station.
    /// </summary>
    /// <returns>A list of <see cref="WeatherReportDto"/> containing the weather report.</returns>
    /// <response code="200">Returns the weather report.</response>
    /// <response code="500">If there is an internal server error.</response>
    /// <response code="404">If the weather report is not found.</response>
    /// /// <response code="400">If the request is invalid.</response>
    [HttpGet("eastweatherstation")]
    [ProducesResponseType(typeof(List<WeatherReportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<WeatherReportDto>> GetWeatherReport()
    {
        var responce = await _eastWeatherStationClient.GetAsync("eastweatherstation");

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
