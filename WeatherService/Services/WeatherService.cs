using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OpenMeteo;


namespace WeatherService.Services;

public class WeatherService : Weather.WeatherBase
{
    private readonly OpenMeteoClient _openMeteoClient;
    public WeatherService(OpenMeteoClient openMeteoClient)
    {
        _openMeteoClient = openMeteoClient;
    }

    public override async Task GetWeatherStream(Empty request, IServerStreamWriter<WeatherData> responseStream,
        ServerCallContext context)
    {
        var options = new WeatherForecastOptions();
        options.Latitude = 35.6895f;
        options.Longitude = 139.69171f;
        options.Past_Days = 92;
        options.Hourly.Add(HourlyOptionsParameter.temperature_2m);
        var resp = await _openMeteoClient.QueryAsync(options);
        var temps = resp.Hourly.Temperature_2m;
        var dates = resp.Hourly.Time;
        for (int i = 0; i < temps.Length; i+=2)
        {
            var data = new WeatherData()
            {
                Temperature = $"{temps[i]} {resp.HourlyUnits.Temperature_2m}", Timestamp = dates[i]
            };
            await responseStream.WriteAsync(data);
            Thread.Sleep(1000);
        }
    }
}