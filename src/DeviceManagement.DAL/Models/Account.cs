using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public class Account
{
    public int Id { get; set; }
    
    [Length(1,50)]
    public string Username { get; set; } = null!;
    
    [Length(1,25)]
    public string Password { get; set; } = null!;
    
    public string EmployeeId { get; set; } = null!;
    public virtual Employee Employee { get; set; } = null!;
    
    public string RoleId { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}