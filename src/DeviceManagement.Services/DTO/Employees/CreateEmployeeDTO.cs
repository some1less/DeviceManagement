using System.ComponentModel.DataAnnotations;
using DeviceManagement.DAL.Models;

namespace DeviceManagement.Services.DTO;

public class CreateEmployeeDTO
{
    [Required]
    public Person Person { get; set; }
    
    [Required]
    public decimal Salary { get; set; }

    [Required]
    public int PositionId { get; set; }
}