using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO.Accounts;
using DeviceManagement.Services.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Rest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DevManagementContext _context;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<Account> _passwordHasher = new();
        
        public AuthController(DevManagementContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Auth(LoginAccountDTO account, CancellationToken cancellationToken)
        {
            var foundAccount = await _context.Accounts.Include(account => account.Role)
                .FirstOrDefaultAsync(a=>string.Equals(a.Username, account.Username), cancellationToken);

            if (foundAccount == null)
            {
                return Unauthorized("Access denied. You either need to register first heheha");
            }
            
            var verificationResult = 
                _passwordHasher.VerifyHashedPassword(foundAccount, foundAccount.Password, account.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Access denied. Wrong password");
            }

            var token = new
            {
                AccessToken = _tokenService.GenerateToken(foundAccount.Username, foundAccount.Role.Name)
            };

            return Ok(token);
        }
    }
}
