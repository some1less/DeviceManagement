using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class GetSpecificAccountDTO
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Role { get; set; }
}