using DeviceManagement.DAL.Models;

namespace DeviceManagement.DAL.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeIdAsync(int empId);
}