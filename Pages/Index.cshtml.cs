using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionConges.Pages;

public class IndexModel : PageModel
{
    private readonly GestionCongesContext _context;

    public IndexModel(GestionCongesContext context)
    {
        _context = context;
    }

    [BindProperty]
    [Required]
    [DataType(DataType.Date)]
    public DateTime? DateDebut { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Date)]
    public DateTime? DateFin { get; set; }

    [BindProperty]
    [Required]
    public string NomEmploye { get; set; }

    [BindProperty]
    [Required]
    public string PosteEmploye { get; set; }

    public List<Conge> Conges { get; set; }

    public string CodeSuiviGenere { get; set; }

    public async Task OnGetAsync()
    {
        Conges = await _context.Conges.OrderByDescending(c => c.Id).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || !DateDebut.HasValue || !DateFin.HasValue || string.IsNullOrWhiteSpace(NomEmploye) || string.IsNullOrWhiteSpace(PosteEmploye))
        {
            await OnGetAsync();
            return Page();
        }

        // Validation: Start date cannot be before current date
        var currentDate = DateTime.Today;
        if (DateDebut.Value.Date < currentDate)
        {
            ModelState.AddModelError("DateDebut", "La date de début ne peut pas être antérieure à la date actuelle.");
            await OnGetAsync();
            return Page();
        }

        // Validation: End date cannot be before start date
        if (DateFin.Value.Date < DateDebut.Value.Date)
        {
            ModelState.AddModelError("DateFin", "La date de fin ne peut pas être antérieure à la date de début.");
            await OnGetAsync();
            return Page();
        }

        var codeSuivi = GenererCodeSuivi();
        var conge = new Conge {
            DateDebut = DateDebut.Value,
            DateFin = DateFin.Value,
            NomEmploye = NomEmploye,
            PosteEmploye = PosteEmploye,
            CodeSuivi = codeSuivi
        };
        _context.Conges.Add(conge);
        await _context.SaveChangesAsync();
        CodeSuiviGenere = codeSuivi;
        await OnGetAsync();
        ModelState.Clear();
        return Page();
    }

    private string GenererCodeSuivi()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
