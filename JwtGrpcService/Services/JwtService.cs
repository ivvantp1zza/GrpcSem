using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grpc.Core;
using Jwtgen;
using JwtGrpcService.Data.Mocks;
using Microsoft.IdentityModel.Tokens;

namespace JwtGrpcService.Services;

public class JwtService : Jwtgen.JwtService.JwtServiceBase
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public override Task<JwtReply> GenerateToken(User request, ServerCallContext context)
    {
        var currentUser = Mocks.Users.FirstOrDefault(x =>
            x.Username ==request.Username &&
            x.Password == request.Password);
        return currentUser is not null ? Task.FromResult(new JwtReply() {Token = GenerateJwtToken(request)}) : 
            base.GenerateToken(request, context);
    }
    
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Username),
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}