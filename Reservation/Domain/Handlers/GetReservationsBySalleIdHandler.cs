// In Domain/Handlers/GetReservationsBySalleIdHandler.cs
using Domain.Interface;
using Domain.Models;
using Domain.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetReservationsBySalleIdHandler : IRequestHandler<GetReservationsBySalleIdQuery, List<Reservations>>
    {
        private readonly IGenericRepository<Reservations> _reservationRepository;

        public GetReservationsBySalleIdHandler(IGenericRepository<Reservations> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<List<Reservations>> Handle(GetReservationsBySalleIdQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Where(r => r.SalleId == request.SalleId).ToList();
        }
    }
}