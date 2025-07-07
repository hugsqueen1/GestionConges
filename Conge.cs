using System;
using System.ComponentModel.DataAnnotations;

public enum StatutConge
{
    EnAttente,
    Validee,
    Refusee,
    Annulee
}

public class Conge
{
    public int Id { get; set; }

    [Required]
    public string NomEmploye { get; set; }

    [Required]
    public string PosteEmploye { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateDebut { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateFin { get; set; }

    public bool Annule { get; set; } = false;

    [Required]
    public string CodeSuivi { get; set; }

    public StatutConge Statut { get; set; } = StatutConge.EnAttente;
} 