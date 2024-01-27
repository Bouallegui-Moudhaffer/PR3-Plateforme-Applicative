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
    public class UtilisateursController : ControllerBase
    {
        private readonly IUtilisateur _utilisateurService;
        private readonly AuthenticationService _authService;
        private readonly TokenService _tokenService;

        public UtilisateursController(IUtilisateur utilisateurService, AuthenticationService authService, TokenService tokenService)
        {
            _utilisateurService = utilisateurService;
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _authService.AuthenticateUser(login);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            var utilisateurs = await _utilisateurService.GetAllAsync();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _utilisateurService.GetByIdAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest();
            }

            try
            {
                await _utilisateurService.UpdateAsync(utilisateur);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UtilisateurExists(id))
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

        // POST: api/Utilisateurs
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            await _utilisateurService.AddAsync(utilisateur);
            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.UtilisateurId }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _utilisateurService.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            await _utilisateurService.DeleteAsync(utilisateur);
            return NoContent();
        }

        private async Task<bool> UtilisateurExists(int id)
        {
            return await _utilisateurService.GetByIdAsync(id) != null;
        }
    }
}
