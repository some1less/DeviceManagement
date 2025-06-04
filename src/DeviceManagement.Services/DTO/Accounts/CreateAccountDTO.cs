using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class CreateAccountDTO
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required]
    [MinLength(12)]
    [MaxLength(25)]
    // [RegularExpression()]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string EmployeeName { get; set; }
}