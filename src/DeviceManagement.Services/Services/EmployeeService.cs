using System.Text.Json;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Services.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DevManagementContext _context;

    public EmployeeService(DevManagementContext context)
    {
        _context = context;
        
    }
    
    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
    {
        var employees = await _context.Employees
            .Include(p => p.Person)
            .ToListAsync();
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
        var employee = await _context.Employees
            .Include(p => p.Person)
            .Include(p => p.Position)
            .FirstOrDefaultAsync(e => e.Id == empId);

        if (employee == null) return null;

        return new EmployeeByIdDTO()
        {
            Person = new PersonGetEmpIdDTO()
            {
                Id = employee.PersonId,
                PassportNumber = employee.Person.PassportNumber,
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