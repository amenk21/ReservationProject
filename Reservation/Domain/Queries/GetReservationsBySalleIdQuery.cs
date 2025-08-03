// In Domain/Queries/GetReservationsBySalleIdQuery.cs
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetReservationsBySalleIdQuery : IRequest<List<Reservations>>
    {
        public Guid SalleId { get; }

        public GetReservationsBySalleIdQuery(Guid salleId)
        {
            SalleId = salleId;
        }
    }
}