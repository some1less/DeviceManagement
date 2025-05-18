using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;

namespace DeviceManagement.Services.DTO;

public class DeviceByIdDTO
{
    public string DeviceTypeName { get; set; } = null!;
    
    public bool IsEnabled { get; set; }

    public Object AdditionalProperties { get; set; } = null!;
    
    public EmployeeDTO? Employee { get; set; }
        
}