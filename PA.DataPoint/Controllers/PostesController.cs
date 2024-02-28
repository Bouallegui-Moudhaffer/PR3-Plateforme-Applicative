using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using PA.ApplicationCore.Models;
using System.Text.RegularExpressions;

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
        [HttpPost]
        public async Task<ActionResult<Postes>> PostPostes(Postes postes)
        {
            await _postesService.AddAsync(postes);
            return CreatedAtAction("GetPostes", new { id = postes.PostesId }, postes);
        }
        // DELETE: api/Postes/5
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
        // Create /api/postes/create
        [HttpPost("Create")]
        public async Task<ActionResult<Postes>> PostSalles(PostesModel model)
        {
            var postes = new Postes
            {
                Ref = model.Ref,
                StatusId = model.StatusId,
                SallesId = model.SallesId,
                TypeId = model.TypeId
            };
            await _postesService.AddAsync(postes);
            return CreatedAtAction("GetPostes", new { id = postes.SallesId }, postes);
        }
        // GET: api/Postes/FindByMac
        [HttpGet("FindByMac")]
        public async Task<ActionResult<Postes>> FindByMac([FromQuery] string macAddress)
        {
            var postes = await _postesService.GetManyAsync(p => p.MacAddress == macAddress);

            var poste = postes.FirstOrDefault();
            if (poste == null)
            {
                return NotFound();
            }
            return poste;
        }
        // PUT: api/Postes/UpdateIpAddress
        [HttpPut("{id}/UpdateIpAddress")]
        public async Task<IActionResult> UpdateIpAddress(int id, [FromBody] string ipAddress)
        {
            var poste = await _postesService.GetByIdAsync(id);
            if (poste == null)
            {
                return NotFound();
            }

            var ipRegex = new Regex(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$");
            if (!ipRegex.IsMatch(ipAddress))
            {
                return BadRequest("Invalid IP address format.");
            }

            poste.IpAddress = ipAddress;

            try
            {
                await _postesService.UpdateAsync(poste);
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
        // PUT: api/Postes/UpdateAggregatedData
        [HttpPut("{id}/UpdateAggregatedData")]
        public async Task<IActionResult> UpdateAggregatedData(int id)
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();

            // Log the raw request body to inspect the received JSON payload
            Console.WriteLine($"Received JSON payload: {requestBody}");

            var model = JsonConvert.DeserializeObject<AggregatedDataModel>(requestBody);

            if (model == null)
            {
                Console.WriteLine("Deserialized model is null.");
                return BadRequest("Invalid request payload.");
            }

            var poste = await _postesService.GetByIdAsync(id);
            if (poste == null)
            {
                return NotFound();
            }

            poste.CpuUsageMean = model.CpuUsageMean;
            poste.CpuUsageMedian = model.CpuUsageMedian;
            poste.CpuUsagePeak = model.CpuUsagePeak;

            poste.MemoryUsageMean = model.MemoryUsageMean;
            poste.MemoryUsageMedian = model.MemoryUsageMedian;
            poste.MemoryUsagePeak = model.MemoryUsagePeak;

            try
            {
                await _postesService.UpdateAsync(poste);
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
    }
}
