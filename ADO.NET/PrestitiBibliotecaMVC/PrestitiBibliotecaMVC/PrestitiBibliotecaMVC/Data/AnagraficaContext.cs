using Microsoft.EntityFrameworkCore;

namespace PrestitiBibliotecaMVC.Data
{
    public class AnagraficaContext : DbContext
    {
        public AnagraficaContext(DbContextOptions<AnagraficaContext> options)
            : base(options)
        {
        }

        // Aggiungi qui DbSet<T> per le entità se necessario, es:
        // public DbSet<Studente> Studenti { get; set; }
    }
}
