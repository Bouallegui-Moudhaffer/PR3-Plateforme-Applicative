using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces; // Include the namespace for ILogService

namespace PA.DataPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILog _logService;

        public LogsController(ILog logService)
        {
            _logService = logService;
        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLog()
        {
            var logs = await _logService.GetAllAsync();
            return Ok(logs);
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(int id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return log;
        }

        // PUT: api/Logs/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(int id, Log log)
        {
            if (id != log.LogId)
            {
                return BadRequest();
            }

            try
            {
                await _logService.UpdateAsync(log);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LogExists(id))
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

        // POST: api/Logs
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Log>> PostLog(Log log)
        {
            await _logService.AddAsync(log);
            return CreatedAtAction("GetLog", new { id = log.LogId }, log);
        }

        // DELETE: api/Logs/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            await _logService.DeleteAsync(log);
            return NoContent();
        }

        private async Task<bool> LogExists(int id)
        {
            return await _logService.GetByIdAsync(id) != null;
        }
    }
}
