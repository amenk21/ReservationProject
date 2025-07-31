using Domain.Commands.ReservationsCommands;
using Domain.Models;
using Domain.Commands;
using Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Handlers.ReservationsHandlers
{
    public class ChangeReservationStatusHandler : IRequestHandler<ChangeReservationsStatusCommand, Reservations?>
    {
        private readonly AppDbContext _context;

        public ChangeReservationStatusHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservations?> Handle(ChangeReservationsStatusCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId && !r.IsDeleted, cancellationToken);

            if (reservation == null) return null;

            if (request.NouveauStatut == StatutReservation.Validée)
            {
                bool conflit = await _context.Reservations
                    .AnyAsync(r =>
                        r.Id != reservation.Id &&
                        r.SalleId == reservation.SalleId &&
                        r.Statut == StatutReservation.Validée &&
                        !r.IsDeleted &&
                        r.DateDebut < reservation.DateFin &&
                        reservation.DateDebut < r.DateFin, cancellationToken);

                if (conflit)
                {
                    throw new InvalidOperationException("Conflit avec une autre réservation déjà validée pour la même salle et période.");
                }
            }

            reservation.Statut = request.NouveauStatut;
            await _context.SaveChangesAsync(cancellationToken);

            return reservation;
        }
    }
}
