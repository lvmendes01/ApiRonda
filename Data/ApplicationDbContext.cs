using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RondaSegurancaBack.Models;

namespace RondaSegurancaBack.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Ronda> Rondas => Set<Ronda>();
        public DbSet<Ocorrencia> Ocorrencias => Set<Ocorrencia>();
    }
}
