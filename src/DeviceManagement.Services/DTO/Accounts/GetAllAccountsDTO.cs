namespace DeviceManagement.Services.DTO.Accounts;

public class GetAllAccountsDTO
{
    public int Id {get; set;}
    public required string Username {get; set;}
    public required string Password {get; set;}
}