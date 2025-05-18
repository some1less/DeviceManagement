using DeviceManagement.Services.DTO;

namespace DeviceManagement.Services.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
    Task<EmployeeByIdDTO?> GetEmployeeIdAsync(int empId);
}