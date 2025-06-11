namespace DeviceManagement.Rest.Middleware.Helpers;

public class ValidationItem
{
    public required string Type { get; set; }
    public required string PreRequestName {get; set;}
    public required string PreRequestValue {get; set;}
    public required List<Rule> Rules {get; set;}
}