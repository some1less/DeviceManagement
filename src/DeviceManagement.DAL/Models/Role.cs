using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public class Role
{
    public int Id { get; set; }
    
    [Required]
    [Length(1,25)]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}