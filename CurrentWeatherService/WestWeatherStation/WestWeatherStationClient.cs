namespace CurrentWeatherService.WestWeatherStation;

/// <summary>
/// Interface for West Weather Station HTTP client.
/// </summary>
public interface IWestWeatherStationHttpClient
{
    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    /// <param name="requestUri">The URI to send the GET request to.</param>
    /// <returns>The HTTP response message.</returns>
    Task<HttpResponseMessage> GetAsync(string requestUri);
}

/// <summary>
/// Client for interacting with the West Weather Station service.
/// </summary>
public class WestWeatherStationClient : IWestWeatherStationHttpClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="WestWeatherStationClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client instance to use.</param>
    public WestWeatherStationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    /// <param name="requestUri">The URI to send the GET request to.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        var uri = new Uri($"{_httpClient.BaseAddress}{requestUri}");
        return await _httpClient.GetAsync(uri);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="WestWeatherStationClient"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }

            _disposedValue = true;
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="WestWeatherStationClient"/>.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
