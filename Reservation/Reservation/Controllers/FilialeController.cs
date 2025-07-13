using Domain.Models;
using Domain.Commands;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Domain.Models;
using Domain.DTOs;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilialeController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddFiliale([FromBody] Filiale filiale) =>
            Ok(await _mediator.Send(new AddGenericCommand<Filiale>(filiale)));

        [HttpGet]
        public async Task<IActionResult> GetAllFiliales() =>
            Ok(await _mediator.Send(new GetGenericQuery<Filiale>()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilialeById(Guid id) =>
            Ok(await _mediator.Send(new GetByIdGenericQuery<Filiale>(id)));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFiliale(Guid id, [FromBody] FilialeUpdateDto dto)
        {
            var existing = await _mediator.Send(new GetByIdGenericQuery<Filiale>(id));
            if (existing == null) return NotFound();

            if (dto.Nom != null) existing.Nom = dto.Nom;
            if (dto.Adresse != null) existing.Adresse = dto.Adresse;
            if (dto.Telephone != null) existing.Telephone = dto.Telephone;

            var result = await _mediator.Send(new PutGenericCommand<Filiale>(id, existing));
            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiliale(Guid id)
        {
            await _mediator.Send(new DeleteGenericCommand<Filiale>(id));
            return NoContent();
        }
    }
}
