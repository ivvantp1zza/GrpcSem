// See https://aka.ms/new-console-template for more information

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using WeatherService;

using var channel = GrpcChannel.ForAddress("http://localhost:5250");
var client = new Weather.WeatherClient(channel);
var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;
using (var call = client.GetWeatherStream(new Empty(), cancellationToken: cancellationToken))
{
    var responseStream = call.ResponseStream;
    await foreach (var resp in responseStream.ReadAllAsync(cancellationToken))
    {
        Console.WriteLine($"Погода на {resp.Timestamp}: {resp.Temperature}");
    }
}

await channel.ShutdownAsync();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();