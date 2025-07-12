// Domain.DTOs/UtilisateurUpdateDto.cs
using Domain.Models;

public class UtilisateurUpdateDto
{
    public string? Nom { get; set; }
    public string? Email { get; set; }
    public string? MotDePasse { get; set; }
    public RoleUtilisateur? Role { get; set; }
}
