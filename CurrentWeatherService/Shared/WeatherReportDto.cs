namespace CurrentWeatherService.Shared;

/// <summary>
/// Represents a weather report with date, temperature in Celsius, and an optional summary.
/// </summary>
/// <param name="Date">The date of the weather report.</param>
/// <param name="TemperatureC">The temperature in Celsius.</param>
/// <param name="Summary">An optional summary of the weather.</param>
public record WeatherReportDto(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
