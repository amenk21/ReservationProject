using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public enum StatutReservation
    {
        EnAttente,
        Validée,
        Refusée,
        Annulée
    }

    public class Reservations : IValidatableObject
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid? SalleId { get; set; }

        [ForeignKey("SalleId")]
        public Salle? Salle { get; set; }

        [Required]
        public Guid? UtilisateurId { get; set; }

        [ForeignKey("UtilisateurId")]
        public Utilisateur? Utilisateur { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [MaxLength(255)]
        public string? Motif { get; set; }

        [Required]
        [EnumDataType(typeof(StatutReservation))]
        public StatutReservation Statut { get; set; } = StatutReservation.EnAttente;

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var tomorrow = DateTime.Today.AddDays(1);

            if (DateDebut.Date < tomorrow)
            {
                yield return new ValidationResult(
                    "La date de début doit être au minimum demain.",
                    new[] { nameof(DateDebut) }
                );
            }

            if (DateFin.Date < tomorrow)
            {
                yield return new ValidationResult(
                    "La date de fin doit être au minimum demain.",
                    new[] { nameof(DateFin) }
                );
            }

            if (DateFin <= DateDebut)
            {
                yield return new ValidationResult(
                    "La date de fin doit être postérieure à la date de début.",
                    new[] { nameof(DateFin) }
                );
            }
        }
    }
}
