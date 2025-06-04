using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class CreateAccountDTO
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}