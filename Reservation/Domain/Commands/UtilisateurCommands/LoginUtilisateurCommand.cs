using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Commands.UtilisateurCommands
{
    public class LoginUtilisateurCommand : IRequest<Utilisateur?>
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        public LoginUtilisateurCommand(string email, string motDePasse)
        {
            Email = email;
            MotDePasse = motDePasse;
        }
    }
}
