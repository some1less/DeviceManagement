using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;

namespace DeviceManagement.Rest.Controllers
{
  
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PasswordHasher<Account> _passwordHasher = new();
        private readonly DevManagementContext _context;

        public AccountController(DevManagementContext context)
        {
            _context = context;
        }

        // GET: api/accounts
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllAccountsDTO>>> GetAccounts()
        {
            var dtoList = await _context.Accounts
                .Select(a => new GetAllAccountsDTO 
                {
                    Id = a.Id,
                    Username = a.Username,
                    Password = a.Password
                })
                .ToListAsync();

            return Ok(dtoList);
        }

        // GET: api/accounts/5
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSpecificAccountDTO>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return new GetSpecificAccountDTO
                {
                    Password = account.Password,
                    Username = account.Username,
                };
            }

            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null || account.Username != user)
                return Forbid();

            return new GetSpecificAccountDTO
            {
                Password = account.Password,
                Username = account.Username,
            };
        }

        // PUT: api/accounts/5
        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, UpdateAccountDTO dto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                account.Username = dto.Username;
                account.Password = _passwordHasher.HashPassword(account, dto.Password);
            }
            
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null || account.Username != user)
                return Forbid();
            
            account.Username = dto.Username;
            account.Password = _passwordHasher.HashPassword(account, dto.Password);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/accounts
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(CreateAccountDTO newAccount)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == newAccount.RoleName);

            if (role == null)
            { 
                throw new Exception($"Role with name {newAccount.RoleName} not found");
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Person.FirstName == newAccount.EmployeeName);
            if (employee == null)
            {
                throw new Exception($"Person with name {newAccount.EmployeeName} not found");
            }

            var account = new Account()
            {
                Username = newAccount.Username,
                Password = newAccount.Password,
                RoleId = role.Id,
                EmployeeId = employee.Id,
            };

            account.Password = _passwordHasher.HashPassword(account, newAccount.Password);
            
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
            
        }

        // DELETE: api/accounts/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
