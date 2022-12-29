using BookCommerceCustom1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookCommerceCustom1.Data
{
    public class Konteksti:IdentityDbContext
    {
        public Konteksti(DbContextOptions<Konteksti> op):base(op)
        {
            
        }

        public DbSet<Kategoria> Kategorite { get; set; }
        public DbSet<Mbulesa> Mbulesa { get; set; }
        public DbSet<Produkti> Produktet { get; set; }
        public DbSet<Shporta> Shportat { get; set; }
        public DbSet<Perdorusi> Perdorusit { get; set; }
        public DbSet<Kompania> Kompania { get; set; }
        public DbSet<Porosia> Porosite { get; set; }
        public DbSet<PorosiDetali> PorosiDetalet { get; set; }
    }
}
