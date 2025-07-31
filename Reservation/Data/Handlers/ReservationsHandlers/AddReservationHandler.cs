using Domain.Commands.ReservationsCommands;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Data.Handlers.ReservationsHandlers
{
    public class AddReservationHandler : IRequestHandler<AddReservationCommand, Reservations>
    {
        private readonly AppDbContext _context;

        public AddReservationHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservations> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var res = request.Reservation;

            // Vérifie s'il y a conflit sur la même salle / période
            bool conflit = await _context.Reservations.AnyAsync(r =>
                r.SalleId == res.SalleId &&
                r.Statut == StatutReservation.Validée &&
                !r.IsDeleted &&
                r.DateDebut < res.DateFin &&
                res.DateDebut < r.DateFin,
                cancellationToken);

            if (conflit)
                throw new InvalidOperationException("Impossible d’ajouter une réservation : conflit détecté avec une réservation existante validée.");

            _context.Reservations.Add(res);
            await _context.SaveChangesAsync(cancellationToken);
            return res;
        }
    }
}
