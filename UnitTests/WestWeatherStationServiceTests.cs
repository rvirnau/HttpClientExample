using System.Net;
using CurrentWeatherService.WestWeatherStation;
using CurrentWeatherService.Shared;
using NSubstitute;
using Shouldly;
using System.Text.Json;

namespace WestWeatherStationServiceUnitTests;

[TestFixture]
public class WestWeatherStationManagerTests
{
    IWestWeatherStationHttpClient _clientMock;
    List<WeatherReportDto> _westWeatherStationReport;

    [SetUp]
    public void Setup()
    {
        _westWeatherStationReport =
            new List<WeatherReportDto>(
            [
                new WeatherReportDto(new DateOnly(2025, 1, 1), 45, "Bracing"),
                    new WeatherReportDto(new DateOnly(2025, 1, 8), 44, "Bracing"),
                    new WeatherReportDto(new DateOnly(2025, 1, 9), 52, "Sweltering")
            ]);

        _clientMock = Substitute.For<IWestWeatherStationHttpClient>();
    }

    [Test]
    public async Task WestWeatherStation_Returns_Expected()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(_westWeatherStationReport))
        };
        _clientMock.GetAsync(Arg.Any<string>()).Returns(responce);
        var westWeatherStationService = new WestWeatherStationService(_clientMock);

        // act
        var result = await westWeatherStationService.GetWeatherReport();

        // assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(_westWeatherStationReport);
    }

    [Test]
    public async Task WestWeatherReport_Returns_Error()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        _clientMock.GetAsync(Arg.Any<string>()).Returns(responce);
        var westWeatherStationService = new WestWeatherStationService(_clientMock);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await westWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error in the response");
    }

    [Test]
    public async Task WestWeatherReport_Returns_ErrorParsing()
    {
        // arrange
        var responce = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Invalid Json")
        };
        _clientMock.GetAsync(Arg.Any<string>()).Returns(responce);
        var westWeatherStationService = new WestWeatherStationService(_clientMock);

        // act and assert
        var exception = await Should.ThrowAsync<Exception>(async () => await westWeatherStationService.GetWeatherReport());
        exception.Message.ShouldBeEquivalentTo("Error parsing responce");
    }
}