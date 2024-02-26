using Grpc.Core;
using JwtGrpcService;
using JwtGrpcService.Data.Mocks;

namespace JwtGrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return base.SayHello(request, context);
    }
}