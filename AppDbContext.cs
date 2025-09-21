using Filmovizija.Models;
using Microsoft.EntityFrameworkCore;


namespace Filmovizija
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admini { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Posudba> Posudbe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=filmovizija.db");
        }

    }
}

