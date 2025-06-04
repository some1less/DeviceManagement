using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeviceManagement.Services.Helpers.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeviceManagement.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtConfig;
    
    public TokenService(IOptions<JwtOptions> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
    }
    
    public string GenerateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Key);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, username),
            new("role", role),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtConfig.ValidInMinutes)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }
}