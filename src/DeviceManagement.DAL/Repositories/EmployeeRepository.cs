using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    
    private readonly DevManagementContext _context;

    public EmployeeRepository(DevManagementContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.
            Include(p => p.Person)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeIdAsync(int empId)
    {
        return await _context.Employees
            .Include(p => p.Person)
            .Include(p => p.Position)
            .FirstOrDefaultAsync(e => e.Id == empId);
    }
}