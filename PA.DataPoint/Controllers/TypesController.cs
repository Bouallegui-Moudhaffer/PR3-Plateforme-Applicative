using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypes _typeService;

        public TypesController(ITypes TypesService)
        {
            _typeService = TypesService;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Types>>> GetTypes()
        {
            var Types = await _typeService.GetAllAsync();
            return Ok(Types);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Types>> GetTypes(int id)
        {
            var salle = await _typeService.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }
            return salle;
        }

        // PUT: api/Types/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypes(int id, Types Types)
        {
            if (id != Types.TypeId)
            {
                return BadRequest();
            }

            try
            {
                await _typeService.UpdateAsync(Types);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TypesExists(id))
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

        // POST: api/Types
        [HttpPost]
        public async Task<ActionResult<Types>> PostTypes(Types Types)
        {
            await _typeService.AddAsync(Types);
            return CreatedAtAction("GetTypes", new { id = Types.TypeId }, Types);
        }

        // DELETE: api/Types/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypes(int id)
        {
            var salle = await _typeService.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }

            await _typeService.DeleteAsync(salle);
            return NoContent();
        }

        private async Task<bool> TypesExists(int id)
        {
            return await _typeService.GetByIdAsync(id) != null;
        }
    }
}
