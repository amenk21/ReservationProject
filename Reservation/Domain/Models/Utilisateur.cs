using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum RoleUtilisateur
    {
        Admin,
        Formateur
    }
    public class Utilisateur
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        public string MotDePasse { get; set; }

        [Required]
        public RoleUtilisateur Role { get; set; }
    }
}
