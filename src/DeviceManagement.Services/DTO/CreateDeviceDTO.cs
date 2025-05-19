using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DeviceManagement.Services.DTO;

public class CreateDeviceDTO
{
    public required string DeviceTypeName { get; set; }
    public required bool IsEnabled { get; set; }
    public JsonElement? AdditionalProperties { get; set; }
}