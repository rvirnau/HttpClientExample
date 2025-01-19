using CurrentWeatherService.NorthWeatherStation;
using CurrentWeatherService.Shared;
using Infrastructure.Shared.Handlers;
using NSubstitute;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace NorthWeatherStationTests;

[TestFixture]
public class NorthWeatherStationManagerTests
{
    List<WeatherReportDto> _northWeatherStationReport;

    [SetUp]
    public void Setup()
    {
        _northWeatherStationReport =
            new List<WeatherReportDto>(
            [
                new WeatherReportDto(new DateOnly(2025, 1, 1), 45, "Bracing"),
                new WeatherReportDto(new DateOnly(2025, 1, 8), 44, "Bracing"),
                new WeatherReportDto(new DateOnly(2025, 1, 9), 52, "Sweltering")
            ]);
    }

    private static NorthWeatherStationService CreateNorthWeatherStationService(HttpResponseMessage responce)
    {
        // create the test http client message handler, and pass the responce
        var testHttpClientMessageHandler = new TestHttpClientMessageHandler(responce);
        // create a mock http client and pass the test http client message handler
        var httpClientMock = Substitute.For<HttpClient>(testHttpClientMessageHandler);
        // the client needs a base address to work
        httpClientMock.BaseAddress = new Uri("http://somewhere.com");
       // create the north weather station service and pass the mock http client factory
        var northWeatherStationService = new NorthWeatherStationService(httpClientMock);

        return northWeatherStationService;
    }

    [Test]
    public async Task NorthWeatherStation_Returns_Expected()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(_northWeatherStationReport))
        };

        NorthWeatherStationService northWeatherStationService = CreateNorthWeatherStationService(responce);

        // act
        var result = await northWeatherStationService.GetWeatherReport();

        // assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(_northWeatherStationReport);
    }

    [Test]
    public async Task NorthWeatherReport_Returns_Error()
    {
        var responce = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        NorthWeatherStationService northWeatherStationService = CreateNorthWeatherStationService(responce);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await northWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error in the response");
    }

    [Test]
    public async Task NorthWeatherReport_Returns_ErrorParsing()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Invalid Json")
        };

        NorthWeatherStationService northWeatherStationService = CreateNorthWeatherStationService(responce);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await northWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error parsing responce");
    }
}
