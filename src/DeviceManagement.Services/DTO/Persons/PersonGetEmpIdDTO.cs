namespace DeviceManagement.Services.DTO;

public class PersonGetEmpIdDTO
{

    public string PassportNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;
}