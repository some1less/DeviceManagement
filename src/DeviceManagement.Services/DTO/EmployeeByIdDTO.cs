using System.Text.Json;
using DeviceManagement.DAL.Models;

namespace DeviceManagement.Services.DTO;

public class EmployeeByIdDTO
{
    public required Person Person { get; set; }
    public decimal Salary { get; set; }
    public PositionDTO Position { get; set; }
    public DateTime HireDate { get; set; }
}