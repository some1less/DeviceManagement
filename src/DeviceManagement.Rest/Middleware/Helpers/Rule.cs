using System.Text.Json;

namespace DeviceManagement.Rest.Middleware.Helpers;

public class Rule
{
    public required string ParamName { get; set; }
    public JsonElement Regex { get; set; }
}