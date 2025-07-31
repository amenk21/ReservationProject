using Domain.Models;
using MediatR;
using System;

namespace Domain.Commands.ReservationsCommands
{
    public class ChangeReservationsStatusCommand : IRequest<Reservations?>
    {
        public Guid ReservationId { get; set; }
        public StatutReservation NouveauStatut { get; set; }

        public ChangeReservationsStatusCommand() { }

        public ChangeReservationsStatusCommand(Guid reservationId, StatutReservation statut)
        {
            ReservationId = reservationId;
            NouveauStatut = statut;
        }
    }
}
