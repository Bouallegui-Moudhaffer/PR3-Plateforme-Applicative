using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using PA.ApplicationCore.Services;

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementsController : ControllerBase
    {
        private readonly IEtablissement _etablissementService;

        public EtablissementsController(IEtablissement etablissementService)
        {
            _etablissementService = etablissementService;
        }

        // GET: api/Etablissements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etablissement>>> GetEtablissements()
        {
            var etablissements = await _etablissementService.GetAllAsync();
            return Ok(etablissements);
        }

        // GET: api/Etablissements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Etablissement>> GetEtablissement(int id)
        {
            var etablissement = await _etablissementService.GetByIdAsync(id);
            if (etablissement == null)
            {
                return NotFound();
            }
            return etablissement;
        }

        // PUT: api/Etablissements/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtablissement(int id, Etablissement etablissement)
        {
            if (id != etablissement.EtablissementId)
            {
                return BadRequest();
            }

            try
            {
                await _etablissementService.UpdateAsync(etablissement);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EtablissementExists(id))
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

        // POST: api/Etablissements
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Etablissement>> PostEtablissement(Etablissement etablissement)
        {
            await _etablissementService.AddAsync(etablissement);
            return CreatedAtAction("GetEtablissement", new { id = etablissement.EtablissementId }, etablissement);
        }

        // DELETE: api/Etablissements/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtablissement(int id)
        {
            var etablissement = await _etablissementService.GetByIdAsync(id);
            if (etablissement == null)
            {
                return NotFound();
            }

            await _etablissementService.DeleteAsync(etablissement);
            return NoContent();
        }

        private async Task<bool> EtablissementExists(int id)
        {
            return await _etablissementService.GetByIdAsync(id) != null;
        }
    }
}
