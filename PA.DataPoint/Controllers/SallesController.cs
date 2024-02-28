using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using PA.ApplicationCore.Models;

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly ISalles _sallesService;

        public SallesController(ISalles sallesService)
        {
            _sallesService = sallesService;
        }

        // GET: api/Salles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salles>>> GetSalles()
        {
            var salles = await _sallesService.GetAllAsync();
            return Ok(salles);
        }

        // GET: api/Salles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salles>> GetSalles(int id)
        {
            var salle = await _sallesService.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }
            return salle;
        }

        // PUT: api/Salles/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalles(int id, Salles salles)
        {
            if (id != salles.SallesId)
            {
                return BadRequest();
            }

            try
            {
                await _sallesService.UpdateAsync(salles);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SallesExists(id))
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

        // POST: api/Salles
        [HttpPost]
        public async Task<ActionResult<Salles>> PostSalles(Salles salles)
        {
            await _sallesService.AddAsync(salles);
            return CreatedAtAction("GetSalles", new { id = salles.SallesId }, salles);
        }

        // DELETE: api/Salles/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalles(int id)
        {
            var salle = await _sallesService.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }

            await _sallesService.DeleteAsync(salle);
            return NoContent();
        }

        private async Task<bool> SallesExists(int id)
        {
            return await _sallesService.GetByIdAsync(id) != null;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Salles>> PostSalles(SallesModel model)
        {
            var salles = new Salles
            {
                Capacite = model.Capacite,
                EtablissementId = model.EtablissementId,
                StatusId = model.StatusId
            };

            await _sallesService.AddAsync(salles);
            return CreatedAtAction("GetSalles", new { id = salles.SallesId }, salles);
        }
    }
}
