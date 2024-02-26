using Jwtgen;

namespace JwtGrpcService.Data.Mocks;

public class Mocks
{
    public static List<User> Users = new()
    {
        new User(){ Username="qwe123",Password="gleblox"}
    };
}