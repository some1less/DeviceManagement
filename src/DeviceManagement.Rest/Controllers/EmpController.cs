using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO;
using DeviceManagement.Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement.Rest.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly DevManagementContext _context;

        public EmpController(DevManagementContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: api/Emp
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/employees/5
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            
            if (User.IsInRole("Admin"))
            {
                return Ok(employee);
            }

            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var account = await _context.Accounts
                .SingleOrDefaultAsync(a => a.Username == user);
            if (account == null)
                return Forbid();

            if (account.EmployeeId != id)
            {
                return Forbid();
            }

            return Ok(employee);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (User.IsInRole("Admin")) {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Username == user);
            if (account == null) return Forbid();

            if (account.EmployeeId != id) return Forbid();

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Emp
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(CreateEmployeeDTO dto)
        {
            
            if (dto.Salary < 0)
                return BadRequest("Salary must be greater than 0");

            var takenPass = await _context.People.AnyAsync(p=>p.PassportNumber==dto.Person.PassportNumber);
            if (takenPass)
            {
                return Conflict($"Some person already has this passport number: {dto.Person.PassportNumber}");
            }
            
            var takenPhone = await _context.People.AnyAsync(p => p.PhoneNumber == dto.Person.PhoneNumber);
            if (takenPhone)
            {
                return Conflict($"Some person already has this phone number: {dto.Person.PhoneNumber}");
            }
            
            var takenEmail = await _context.People.AnyAsync(p => p.Email == dto.Person.Email);
            if (takenEmail)
            {
                return Conflict($"Some person already has this email: {dto.Person.Email}");
            }
            
            var position = await _context.Positions.SingleOrDefaultAsync(p => p.Id == dto.PositionId);
            if (position == null)
                return NotFound($"Position with id={dto.PositionId} not found");

            Employee employee = new Employee()
            {
                Person = dto.Person,
                Salary = dto.Salary,
                PositionId = dto.PositionId,
            };
                
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Emp/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
