using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class CreateAccountDTO
{
    [Required]
    [MaxLength(50)]
    [RegularExpression("^[A-Za-z].*$", 
        ErrorMessage="Username must start with a letter!")]
    public string Username { get; set; }
    
    [Required]
    [MinLength(12)]
    [MaxLength(25)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
        ErrorMessage = "Password must contain at least one small letter, one capital letter, one number and one symbol!")]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string EmployeeName { get; set; }
}