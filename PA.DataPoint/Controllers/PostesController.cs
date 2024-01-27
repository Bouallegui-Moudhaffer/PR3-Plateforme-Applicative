using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces; // Include the namespace for IPostesService

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostesController : ControllerBase
    {
        private readonly IPostes _postesService;

        public PostesController(IPostes postesService)
        {
            _postesService = postesService;
        }

        // GET: api/Postes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postes>>> GetPostes()
        {
            var postes = await _postesService.GetAllAsync();
            return Ok(postes);
        }

        // GET: api/Postes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postes>> GetPostes(int id)
        {
            var poste = await _postesService.GetByIdAsync(id);
            if (poste == null)
            {
                return NotFound();
            }
            return poste;
        }

        // PUT: api/Postes/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostes(int id, Postes postes)
        {
            if (id != postes.PostesId)
            {
                return BadRequest();
            }

            try
            {
                await _postesService.UpdateAsync(postes);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PostesExists(id))
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

        // POST: api/Postes
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Postes>> PostPostes(Postes postes)
        {
            await _postesService.AddAsync(postes);
            return CreatedAtAction("GetPostes", new { id = postes.PostesId }, postes);
        }

        // DELETE: api/Postes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostes(int id)
        {
            var poste = await _postesService.GetByIdAsync(id);
            if (poste == null)
            {
                return NotFound();
            }

            await _postesService.DeleteAsync(poste);
            return NoContent();
        }

        private async Task<bool> PostesExists(int id)
        {
            return await _postesService.GetByIdAsync(id) != null;
        }
    }
}
