using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.Accounts;

public class UpdateAccountDTO
{
    [Required]
    [MaxLength(50)]
    [RegularExpression("^[A-Za-z].*$", 
        ErrorMessage="Username must start with a letter!")]
    public required string Username { get; set; }
    
    [Required]
    [MinLength(12)]
    [MaxLength(25)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
        ErrorMessage = "Password must contain at least one small letter, one capital letter, one number and one symbol!")]
    public required string Password { get; set; }
}