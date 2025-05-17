namespace DeviceManagement.DAL.Models;

public class Employee
{
    public int Id {get;set;}
    public decimal Salary {get;set;}
    public int PositionId {get;set;}
    public int PersonId {get;set;}
    public DateTime HireDate {get;set;}
    
}