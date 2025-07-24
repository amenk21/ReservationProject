using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Queries
{
    public class GetSalleByFilialeIdQuery : IRequest<List<Salle>>
    {
        public Guid FilialeId { get; set; }

        public GetSalleByFilialeIdQuery(Guid filialeId)
        {
            FilialeId = filialeId;
        }
    }


}
