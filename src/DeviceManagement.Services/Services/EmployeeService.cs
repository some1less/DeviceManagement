using System.Text.Json;
using DeviceManagement.DAL.Models;
using DeviceManagement.DAL.Repositories;
using DeviceManagement.Services.DTO;

namespace DeviceManagement.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        var dtos = new List<EmployeeDTO>();

        foreach (var employee in employees)
        {
            dtos.Add(new EmployeeDTO()
            {
                Id = employee.Id,
                Name = $"{employee.Person.FirstName} {employee.Person.MiddleName} {employee.Person.LastName}"
            });
        }
        
        return dtos;
    }

    public async Task<EmployeeByIdDTO?> GetEmployeeIdAsync(int empId)
    {
        var employee = await _employeeRepository.GetEmployeeIdAsync(empId);
        if (employee == null) return null;

        return new EmployeeByIdDTO()
        {
            Person = new PersonGetEmpIdDTO()
            {
                Id = employee.PersonId,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                LastName = employee.Person.LastName,
                PhoneNumber = employee.Person.PhoneNumber,
                Email = employee.Person.Email,
            },
            Salary = employee.Salary,
            Position = new PositionDTO()
            {
                Id = employee.PositionId,
                Name = employee.Position.Name
            },
            HireDate = employee.HireDate
        };
    }
}