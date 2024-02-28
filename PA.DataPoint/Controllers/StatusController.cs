using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatus _statuservice;

        public StatusController(IStatus StatusService)
        {
            _statuservice = StatusService;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            var Status = await _statuservice.GetAllAsync();
            return Ok(Status);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var salle = await _statuservice.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }
            return salle;
        }

        // PUT: api/Status/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status Status)
        {
            if (id != Status.StatusId)
            {
                return BadRequest();
            }

            try
            {
                await _statuservice.UpdateAsync(Status);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StatusExists(id))
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

        // POST: api/Status
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status Status)
        {
            await _statuservice.AddAsync(Status);
            return CreatedAtAction("GetStatus", new { id = Status.StatusId }, Status);
        }

        // DELETE: api/Status/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var salle = await _statuservice.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }

            await _statuservice.DeleteAsync(salle);
            return NoContent();
        }

        private async Task<bool> StatusExists(int id)
        {
            return await _statuservice.GetByIdAsync(id) != null;
        }
    }
}
