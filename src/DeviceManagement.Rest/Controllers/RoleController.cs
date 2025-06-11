using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement.Rest.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DevManagementContext _context;

        public RoleController(DevManagementContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        
    }
}
