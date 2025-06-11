using System.Text.Json;
using DeviceManagement.DAL.Models;

namespace DeviceManagement.Services.DTO;

public class EmployeeByIdDTO
{
    public required PersonGetEmpIdDTO Person { get; set; }
    public decimal Salary { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
}