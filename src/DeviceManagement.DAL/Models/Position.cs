namespace DeviceManagement.DAL.Models;

public class Position
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int MinExpYears { get; set; }
}