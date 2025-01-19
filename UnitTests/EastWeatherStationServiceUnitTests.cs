using CurrentWeatherService.EastWeatherStation;
using CurrentWeatherService.Shared;
using Infrastructure.Shared.Handlers;
using NSubstitute;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace EastWeatherStationTests;

[TestFixture]
public class EastWeatherStationManagerTests
{
    List<WeatherReportDto> _eastWeatherStationReport;

    [SetUp]
    public void Setup()
    {
        _eastWeatherStationReport =
            new List<WeatherReportDto>(
            [
                new WeatherReportDto(new DateOnly(2025, 1, 1), 45, "Bracing"),
                new WeatherReportDto(new DateOnly(2025, 1, 8), 44, "Bracing"),
                new WeatherReportDto(new DateOnly(2025, 1, 9), 52, "Sweltering")
            ]);
    }

    private static EastWeatherStationService CreateEastWeatherStationService(HttpResponseMessage responce)
    {
        // create the test http client message handler, and pass the responce
        var testHttpClientMessageHandler = new TestHttpClientMessageHandler(responce);
        // create a mock of the http client factory
        var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
        // create a mock http client and pass the test http client message handler
        var httpClientMock = Substitute.For<HttpClient>(testHttpClientMessageHandler);
        // the client needs a base address to work
        httpClientMock.BaseAddress = new Uri("http://somewhere.com");
        // reconfigure the http client factory's CreateClient method to return the mock named http client
        httpClientFactoryMock.CreateClient("eastWeatherStationHttpClient").Returns(httpClientMock);
        // create the east weather station service and pass the mock http client factory
        var eastWeatherStationService = new EastWeatherStationService(httpClientFactoryMock);

        return eastWeatherStationService;
    }

    [Test]
    public async Task EastWeatherStation_Returns_Expected()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(_eastWeatherStationReport))
        };

        EastWeatherStationService eastWeatherStationService = CreateEastWeatherStationService(responce);

        // act
        var result = await eastWeatherStationService.GetWeatherReport();

        // assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(_eastWeatherStationReport);
    }

    [Test]
    public async Task EastWeatherReport_Returns_Error()
    {
        var responce = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        EastWeatherStationService eastWeatherStationService = CreateEastWeatherStationService(responce);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await eastWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error in the response");
    }

    [Test]
    public async Task EastWeatherReport_Returns_ErrorParsing()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Invalid Json")
        };

        EastWeatherStationService eastWeatherStationService = CreateEastWeatherStationService(responce);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await eastWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error parsing responce");
    }
}
