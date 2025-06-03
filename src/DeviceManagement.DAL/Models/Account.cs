namespace DeviceManagement.DAL.Models;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string EmployeeId { get; set; } = null!;
    public virtual Employee Employee { get; set; } = null!;
    
    public string RoleId { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}