using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeviceManagement.Services.DTO;

public class UpdateDeviceDTO
{
    [Required]
    [JsonPropertyName("name")]
    public string DeviceName {get;set;}
    
    [Required]
    [JsonPropertyName("isEnabled")]
    public bool IsEnabled { get; set; }
    
    [JsonPropertyName("additionalProperties")]
    public JsonElement? AdditionalProperties { get; set; }
    
    [Required]
    [JsonPropertyName("typeId")]
    public int TypeId { get; set; }
}