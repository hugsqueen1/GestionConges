using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class DashboardAdminModel : PageModel
{
    private readonly GestionCongesContext _context;
    public DashboardAdminModel(GestionCongesContext context) { _context = context; }

    public List<Conge> Conges { get; set; }
    public int TotalDemandes { get; set; }
    public int EnAttente { get; set; }
    public int Validees { get; set; }
    public int Refusees { get; set; }
    public int Annulees { get; set; }

    public async Task OnGetAsync()
    {
        Conges = await _context.Conges.OrderByDescending(c => c.Id).ToListAsync();
        TotalDemandes = Conges.Count;
        EnAttente = Conges.Count(c => c.Statut == StatutConge.EnAttente);
        Validees = Conges.Count(c => c.Statut == StatutConge.Validee);
        Refusees = Conges.Count(c => c.Statut == StatutConge.Refusee);
        Annulees = Conges.Count(c => c.Statut == StatutConge.Annulee);
    }

    public async Task OnPostValiderAsync(int id)
    {
        var conge = await _context.Conges.FindAsync(id);
        if (conge != null && conge.Statut == StatutConge.EnAttente)
        {
            conge.Statut = StatutConge.Validee;
            await _context.SaveChangesAsync();
        }
        await OnGetAsync();
    }

    public async Task OnPostRefuserAsync(int id)
    {
        var conge = await _context.Conges.FindAsync(id);
        if (conge != null && conge.Statut == StatutConge.EnAttente)
        {
            conge.Statut = StatutConge.Refusee;
            await _context.SaveChangesAsync();
        }
        await OnGetAsync();
    }

    public async Task OnPostSupprimerAsync(int id)
    {
        var conge = await _context.Conges.FindAsync(id);
        if (conge != null)
        {
            _context.Conges.Remove(conge);
            await _context.SaveChangesAsync();
        }
        await OnGetAsync();
    }

    public async Task<IActionResult> OnPostDownloadCsvAsync(string statut)
    {
        try
        {
            var congesQuery = _context.Conges.AsQueryable();
            if (!string.IsNullOrEmpty(statut) && statut != "Tous")
            {
                if (Enum.TryParse<StatutConge>(statut, out var statutEnum))
                {
                    congesQuery = congesQuery.Where(c => c.Statut == statutEnum);
                }
            }
            var conges = await congesQuery.OrderByDescending(c => c.Id).ToListAsync();
            var csv = "Nom;Poste;DateDebut;DateFin;Statut;CodeSuivi\n" +
                string.Join("\n", conges.Select(c => $"{c.NomEmploye};{c.PosteEmploye};{c.DateDebut:yyyy-MM-dd};{c.DateFin:yyyy-MM-dd};{c.Statut};{c.CodeSuivi}"));
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "demandes_conges.csv");
        }
        catch (Exception ex)
        {
            // Log l'erreur ici si besoin
            return Content($"Erreur lors de la génération du fichier : {ex.Message}");
        }
    }
} 