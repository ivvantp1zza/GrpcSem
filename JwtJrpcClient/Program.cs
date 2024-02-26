// See https://aka.ms/new-console-template for more information

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Jwtgen;
using Secret;

var channel = GrpcChannel.ForAddress("http://localhost:5172");
var jwtClient = new JwtService.JwtServiceClient(channel);
var jwt = await jwtClient.GenerateTokenAsync(new User(){Username = "qwe123", Password = "gleblox"});

var headers = new Metadata
{
    { "Authorization", $"Bearer {jwt.Token}" }
};

var secretClient = new SecretInfoService.SecretInfoServiceClient(channel);

var res = await secretClient.GetSecretInfoAsync(
    new Empty(), headers);

Console.WriteLine(res);
