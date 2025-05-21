using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DeviceManagement.Services.DTO;

public class UpdateDeviceDTO
{
    public required string Name {get;set;}
    public required string DeviceTypeName { get; set; }
    public required bool IsEnabled { get; set; }
    public required JsonElement? AdditionalProperties { get; set; }
}