using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class GestionCongesContext : IdentityDbContext
{
    public GestionCongesContext(DbContextOptions<GestionCongesContext> options)
        : base(options)
    {
    }

    public DbSet<Conge> Conges { get; set; }
} 