using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSalle([FromBody] Salle salle)
        {
            return Ok(await _mediator.Send(new AddGenericCommand<Salle>(salle)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalles()
        {
            return Ok(await _mediator.Send(new GetGenericQuery<Salle>()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalleById(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdGenericQuery<Salle>(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalle(Guid id, [FromBody] SalleUpdateDto dto)
        {
            var existing = await _mediator.Send(new GetByIdGenericQuery<Salle>(id));
            if (existing == null) return NotFound();

            if (dto.Nom != null) existing.Nom = dto.Nom;
            if (dto.Capacite.HasValue) existing.Capacite = dto.Capacite.Value;
            if (dto.Statut.HasValue) existing.Statut = dto.Statut.Value;
            if (dto.FilialeId.HasValue) existing.FilialeId = dto.FilialeId.Value;

            return Ok(await _mediator.Send(new PutGenericCommand<Salle>(id, existing)));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalle(Guid id)
        {
            await _mediator.Send(new DeleteGenericCommand<Salle>(id));
            return NoContent();
        }
    }
}
