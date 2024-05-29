using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Buch.Models;

namespace Buch.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kassenbuch> Kassenbuch { get; set; }
        public DbSet<Kategorie> Kategorie { get; set;}
        public DbSet<Rechnung> Rechnung { get; set; }
        public DbSet<RechnungKategorie> RechnungKategorie { get; set; }
    }
}
