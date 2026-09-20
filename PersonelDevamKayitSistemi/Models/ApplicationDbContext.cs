using Microsoft.EntityFrameworkCore;

namespace PersonelKayitSistemi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }

        public DbSet<Hareket> Hareketler { get; set; }

        public DbSet<IzinTalebi> IzinTalepleri { get; set; }
    }
}