using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Services.DTO.DeviceTypes;

public class GetAllDeviceTypesDTO
{
    [Required]
    public int Id{ get; set; }
    
    [Required]
    public string Name { get; set; }
}