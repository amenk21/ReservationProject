using Data.Context;
using Domain.Commands;
using Domain.Commands.UtilisateurCommands;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public UtilisateurController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                return BadRequest("Utilisateur is null.");

            var existingUser = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == utilisateur.Email);

            if (existingUser != null)
                return Conflict("Cet email existe déjà.");
            var command = new AddGenericCommand<Utilisateur>(utilisateur);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllUtilisateurs()
        {
            var query = new GetGenericQuery<Utilisateur>();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateurById(Guid id)
        {
            var query = new GetByIdGenericQuery<Utilisateur>(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(Guid id, [FromBody] UtilisateurUpdateDto dto)
        {
            var existing = await _mediator.Send(new GetByIdGenericQuery<Utilisateur>(id));
            if (existing == null) return NotFound();

            if (dto.Nom != null) existing.Nom = dto.Nom;
            if (dto.Email != null) existing.Email = dto.Email;
            if (dto.MotDePasse != null) existing.MotDePasse = dto.MotDePasse;
            if (dto.Role != null) existing.Role = dto.Role.Value;

            var result = await _mediator.Send(new PutGenericCommand<Utilisateur>(id, existing));
            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(Guid id)
        {
            await _mediator.Send(new DeleteGenericCommand<Utilisateur>(id));
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUtilisateurCommand command)
        {
            var user = await _mediator.Send(command);

            if (user == null)
                return Unauthorized("Email ou mot de passe incorrect.");

            return Ok(new
            {
                user.Id,
                user.Nom,
                user.Email,
                user.Role
            });
        }
    


}
}
