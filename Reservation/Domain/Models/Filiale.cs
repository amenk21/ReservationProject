using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace Domain.Models
    {
        public class Filiale
        {
            [Key]
            public Guid Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Nom { get; set; }

            [MaxLength(250)]
            public string Adresse { get; set; }

            public string Telephone { get; set; }

            public bool IsDeleted { get; set; } = false;
        }
    }

}
