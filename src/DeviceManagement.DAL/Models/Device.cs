using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.DAL.Models;

public class Device
{
    public int Id {get;set;}
    public required string Name {get;set;}
    public bool IsEnabled {get;set;}
    public required string AdditionalProperties {get;set;}
    public int? DeviceType {get;set;}
}