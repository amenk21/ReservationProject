using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UtilisateurController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUtilisateur( Utilisateur utilisateur)
        {
            if (utilisateur == null)
                return BadRequest("Utilisateur is null.");

            var command = new AddGenericCommand<Utilisateur>(utilisateur);
            var result = await _mediator.Send(command);
            return Ok(result); // returns the saved user
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






    }
}
