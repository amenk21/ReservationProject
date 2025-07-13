using Domain.Models;

public class SalleUpdateDto
{
    public string? Nom { get; set; }
    public int? Capacite { get; set; }
    public StatutSalle? Statut { get; set; }
    public Guid? FilialeId { get; set; }
}
