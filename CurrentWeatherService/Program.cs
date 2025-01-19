using CurrentWeatherService.EastWeatherStation;
using CurrentWeatherService.NorthWeatherStation;
using CurrentWeatherService.Shared;
using CurrentWeatherService.WestWeatherStation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWestWeatherStationService, WestWeatherStationService>();

builder.Services.AddHttpClient<IWestWeatherStationHttpClient, WestWeatherStationClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5251");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<INorthWeatherStationService, NorthWeatherStationService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5015");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<IEastWeatherStationService, EastWeatherStationService>();

builder.Services.AddHttpClient("eastWeatherStationHttpClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5188");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseHttpsRedirection();

app.AddEndpoints();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
