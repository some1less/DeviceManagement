using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO;
using DeviceManagement.Services.Services;
using DeviceManagement.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement.Rest.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly DevManagementContext _context;

        public DeviceController(IDeviceService deviceService, DevManagementContext context)
        {
            _deviceService = deviceService;
            _context = context;
        }

        // GET: api/devices
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            try
            {
                var devices = await _deviceService.GetAllDevicesAsync();
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/devices/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            try
            {
                var device = await _deviceService.GetDeviceIdAsync(id);
                if (device == null)
                    return NotFound();

                if (User.IsInRole("Admin"))
                {
                    return Ok(device);
                }

                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var account = await _context.Accounts
                    .SingleOrDefaultAsync(a => a.Username == user);
                if (account == null)
                    return Forbid();
                
                var employeeId = account.EmployeeId;
                var isAssigned = await _context.DeviceEmployees
                    .AnyAsync(de => de.DeviceId == id && de.EmployeeId == employeeId);

                if (!isAssigned)
                {
                    return Forbid();
                }
                
                return Ok(device);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("types")]
        public async Task<IActionResult> GetDeviceTypes()
        {
            try
            {
                var devices = await _deviceService.GetAllDeviceTypesAsync();
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // POST: api/devices
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostDevice(CreateDeviceDTO deviceDto)
        {
            try
            {
                var created = await _deviceService.CreateDeviceAsync(deviceDto);
                return CreatedAtAction(nameof(GetDevice), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/devices/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, UpdateDeviceDTO deviceDto)
        {
            try
            {
                var existing = await _deviceService.GetDeviceIdAsync(id);
                if (existing == null)
                    return NotFound();
                
                if (User.IsInRole("Admin"))
                {
                    await _deviceService.UpdateDeviceAsync(id, deviceDto);
                    return NoContent();
                }

                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var account = await _context.Accounts
                    .SingleOrDefaultAsync(a => a.Username == user);
                if (account == null)
                    return Forbid();
                
                var employeeId = account.EmployeeId;
                var isAssigned = await _context.DeviceEmployees
                    .AnyAsync(de => de.DeviceId == id && de.EmployeeId == employeeId);

                if (!isAssigned)
                {
                    return Forbid();
                }
                
                await _deviceService.UpdateDeviceAsync(id, deviceDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/devices/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            try
            {
                var existing = await _deviceService.GetDeviceIdAsync(id);
                if (existing == null)
                    return NotFound();

                await _deviceService.DeleteDeviceAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}