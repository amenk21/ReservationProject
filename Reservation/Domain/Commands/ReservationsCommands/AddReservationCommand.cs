using Domain.Models;
using MediatR;

namespace Domain.Commands.ReservationsCommands
{
    public class AddReservationCommand : IRequest<Reservations>
    {
        public Reservations Reservation { get; }

        public AddReservationCommand(Reservations reservation)
        {
            Reservation = reservation;
        }
    }
}
