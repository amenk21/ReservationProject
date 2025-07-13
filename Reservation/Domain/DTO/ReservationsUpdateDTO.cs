using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ReservationsUpdateDto
    {
        public Guid? SalleId { get; set; }

        public Guid? UtilisateurId { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public string? Motif { get; set; }

        public StatutReservation? Statut { get; set; }
    }
}
