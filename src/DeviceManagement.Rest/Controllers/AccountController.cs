using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        // GET: api/User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/User/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

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
        [Authorize]
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

        // DELETE: api/User/5
        [Authorize]
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
