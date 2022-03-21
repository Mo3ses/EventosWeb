using Eventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventos.Persistence.Data
{
    public class EventosDataContext : DbContext
    {
        public EventosDataContext(DbContextOptions<EventosDataContext> options) 
            : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new {PE.EventoId, PE.PalestranteId}); //Associação
        
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedeSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade); //Delete Cascade

            // modelBuilder.Entity<Palestrante>()
            //     .HasMany(e => e.RedeSociais)
            //     .WithOne(rs => rs.Palestrante)
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}