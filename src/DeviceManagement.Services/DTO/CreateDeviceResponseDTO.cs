using System.Text.Json;

namespace DeviceManagement.Services.DTO;

public class CreateDeviceResponseDTO
{
    public int Id {get;set;}
    public required string DeviceTypeName { get; set; }
    public bool IsEnabled { get; set; }
    public JsonElement? AdditionalProperties { get; set; }
}