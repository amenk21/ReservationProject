using Domain.Interface;
using Domain.Models;
using Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class GetSalleByFilialeIdHandler : IRequestHandler<GetSalleByFilialeIdQuery, List<Salle>>
    {
        private readonly IGenericRepository<Salle> _salleRepository;

        public GetSalleByFilialeIdHandler(IGenericRepository<Salle> salleRepository)
        {
            _salleRepository = salleRepository;
        }

        public async Task<List<Salle>> Handle(GetSalleByFilialeIdQuery request, CancellationToken cancellationToken)
        {
            var salles = await _salleRepository.GetAllAsync(); // Pas de paramètre
            return salles.Where(s => s.FilialeId == request.FilialeId).ToList(); // Filtrage en mémoire
        }
    }


}
