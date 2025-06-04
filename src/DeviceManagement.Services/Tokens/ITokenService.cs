using DeviceManagement.DAL.Models;

namespace DeviceManagement.Services.Tokens;

public interface ITokenService
{
    string GenerateToken(string username, string role);
}