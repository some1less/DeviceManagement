using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeviceManagement.DAL.Models;

public class Role
{
    public int Id { get; set; }
    
    [Required]
    [Length(1,25)]
    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}