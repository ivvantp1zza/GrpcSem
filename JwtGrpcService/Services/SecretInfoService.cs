using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Secret;

namespace JwtGrpcService.Services;

[Authorize]
public class SecretInfoService : Secret.SecretInfoService.SecretInfoServiceBase
{
    public override Task<SecretInfo> GetSecretInfo(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new SecretInfo(){Secret= "GLEB LOX"});
    }
}