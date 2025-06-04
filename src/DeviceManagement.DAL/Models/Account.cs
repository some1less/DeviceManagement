using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public class Account
{
    public int Id { get; set; }
    
    [Required]
    [Length(1,50)]
    public string Username { get; set; } = null!;
    
    [Required]
    [Length(1,25)]
    public string Password { get; set; } = null!;
    
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; } = null!;
    
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}