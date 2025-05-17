namespace DeviceManagement.DAL.Models;

public class Person
{
    public int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}