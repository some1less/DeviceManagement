using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class LoginAccountDTO
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}