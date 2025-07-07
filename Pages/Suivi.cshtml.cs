using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class SuiviModel : PageModel
{
    private readonly GestionCongesContext _context;
    public SuiviModel(GestionCongesContext context) { _context = context; }

    [BindProperty]
    [Required]
    public string CodeSuivi { get; set; }

    public Conge CongeTrouve { get; set; }
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        CongeTrouve = await _context.Conges.FirstOrDefaultAsync(c => c.CodeSuivi == CodeSuivi);
        if (CongeTrouve == null)
        {
            Message = "Aucune demande trouvée pour ce code.";
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAnnulerAsync()
    {
        CongeTrouve = await _context.Conges.FirstOrDefaultAsync(c => c.CodeSuivi == CodeSuivi);
        if (CongeTrouve != null && CongeTrouve.Statut == StatutConge.EnAttente)
        {
            CongeTrouve.Statut = StatutConge.Annulee;
            await _context.SaveChangesAsync();
            Message = "Votre demande a été annulée.";
        }
        return Page();
    }

    public async Task<IActionResult> OnPostModifierAsync(DateTime dateDebut, DateTime dateFin)
    {
        CongeTrouve = await _context.Conges.FirstOrDefaultAsync(c => c.CodeSuivi == CodeSuivi);
        if (CongeTrouve != null && CongeTrouve.Statut == StatutConge.EnAttente)
        {
            // Validation: Start date cannot be before current date
            var currentDate = DateTime.Today;
            if (dateDebut.Date < currentDate)
            {
                Message = "Erreur : La date de début ne peut pas être antérieure à la date actuelle.";
                return Page();
            }

            // Validation: End date cannot be before start date
            if (dateFin.Date < dateDebut.Date)
            {
                Message = "Erreur : La date de fin ne peut pas être antérieure à la date de début.";
                return Page();
            }

            CongeTrouve.DateDebut = dateDebut;
            CongeTrouve.DateFin = dateFin;
            await _context.SaveChangesAsync();
            Message = "Votre demande a été modifiée avec succès.";
        }
        return Page();
    }
} 