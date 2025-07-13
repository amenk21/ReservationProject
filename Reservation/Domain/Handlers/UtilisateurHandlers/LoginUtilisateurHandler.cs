using Domain.Commands.UtilisateurCommands;
using Domain.Interface;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.UtilisateurHandlers
{
    public class LoginUtilisateurHandler : IRequestHandler<LoginUtilisateurCommand, Utilisateur?>
    {
        private readonly IGenericRepository<Utilisateur> _repository;

        public LoginUtilisateurHandler(IGenericRepository<Utilisateur> repository)
        {
            _repository = repository;
        }

        public async Task<Utilisateur?> Handle(LoginUtilisateurCommand request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return users.FirstOrDefault(u =>
                u.Email == request.Email &&
                u.MotDePasse == request.MotDePasse &&
                !u.IsDeleted
            );
        }
    }
}
