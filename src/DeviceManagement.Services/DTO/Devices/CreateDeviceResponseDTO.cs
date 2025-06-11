using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DeviceManagement.Services.DTO;

public class CreateDeviceResponseDTO
{
    public int Id {get;set;}
    public required string DeviceName {get;set;}
    public required string DeviceTypeName { get; set; }
    public required bool IsEnabled { get; set; }
    public JsonElement? AdditionalProperties { get; set; }
}