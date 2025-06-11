using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.DAL.Context;
using DeviceManagement.DAL.Models;
using DeviceManagement.Services.DTO;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement.Rest.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly DevManagementContext _context;

        public PositionController(DevManagementContext context)
        {
            _context = context;
        }

        // GET: api/Position
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDTO>>> GetPositions()
        {
            try
            {
                var positions = await _context.Positions.ToListAsync();
                var dtos = new List<PositionDTO>();
                foreach (var position in positions)
                {
                    dtos.Add(new PositionDTO()
                    {
                        Id = position.Id,
                        Name = position.Name,
                    });
                }

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Position/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDTO>> GetPosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            var positionDto = new PositionDTO()
            {
                Id = position.Id,
                Name = position.Name,
            };

            return positionDto;
        }

        
    }
}
