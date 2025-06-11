using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;

namespace DeviceManagement.Services.DTO;

public class DeviceByIdDTO
{

    public string Name { get; set; }
    public bool IsEnabled { get; set; }

    public Object AdditionalProperties { get; set; } = null!;
    
    public string Type { get; set; } = null!;
        
}