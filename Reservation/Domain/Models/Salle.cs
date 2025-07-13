using Domain.Models.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public enum StatutSalle
    {
        Disponible,
        Réservée,
        EnMaintenance
    }

    public class Salle
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacite { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public StatutSalle Statut { get; set; } = StatutSalle.Disponible;

        public Guid FilialeId { get; set; }

        [ForeignKey("FilialeId")]
        public Filiale? Filiale { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
