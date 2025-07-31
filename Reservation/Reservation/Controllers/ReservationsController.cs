using Domain.Commands;
using Domain.DTO;
using Domain.Models;
using Domain.DTO;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.ReservationsCommands;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Reservations
        /* [HttpPost]
         public async Task<IActionResult> AddReservation([FromBody] Reservations reservation)
         {
             var result = await _mediator.Send(new AddGenericCommand<Reservations>(reservation));
             return Ok(result);
         }*/
        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] Reservations reservation)
        {
            var result = await _mediator.Send(new AddReservationCommand(reservation));
            return Ok(result);
        }


        // GET: api/Reservations
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _mediator.Send(new GetGenericQuery<Reservations>());
            return Ok(result);
        }

        // GET: api/Reservations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var result = await _mediator.Send(new GetByIdGenericQuery<Reservations>(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // PUT: api/Reservations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationsUpdateDto dto)
        {
            var existing = await _mediator.Send(new GetByIdGenericQuery<Reservations>(id));
            if (existing == null) return NotFound();

            if (dto.SalleId.HasValue) existing.SalleId = dto.SalleId.Value;
            if (dto.UtilisateurId.HasValue) existing.UtilisateurId = dto.UtilisateurId.Value;
            if (dto.DateDebut.HasValue) existing.DateDebut = dto.DateDebut.Value;
            if (dto.DateFin.HasValue) existing.DateFin = dto.DateFin.Value;
            if (dto.Motif != null) existing.Motif = dto.Motif;
            if (dto.Statut.HasValue) existing.Statut = dto.Statut.Value;

            var result = await _mediator.Send(new PutGenericCommand<Reservations>(id, existing));
            return Ok(result);
        }


        // DELETE: api/Reservations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            await _mediator.Send(new DeleteGenericCommand<Reservations>(id));
            return NoContent();
        }

        [HttpPut("changer-statut")]
        public async Task<IActionResult> ChangerStatut([FromBody] ChangeReservationsStatusCommand command)
        {
            if (command == null)
                return BadRequest("Commande invalide.");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound("Réservation non trouvée ou erreur de mise à jour.");

            return Ok(result);
        }







    }
}
