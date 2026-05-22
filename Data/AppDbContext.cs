using Microsoft.EntityFrameworkCore;
using W5iAtendimentoAPI.Models;

namespace W5iAtendimentoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setor> Setores { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setor>().HasData(
                new Setor { Id = 1, Nome = "TI" },
                new Setor { Id = 2, Nome = "RH" },
                new Setor { Id = 3, Nome = "Financeiro" },
                new Setor { Id = 4, Nome = "Manutenção" }
            );

            modelBuilder.Entity<Prioridade>().HasData(
                new Prioridade { Id = 1, Descricao = "Baixa", PrazoHoras = 48 },
                new Prioridade { Id = 2, Descricao = "Média", PrazoHoras = 24 },
                new Prioridade { Id = 3, Descricao = "Alta", PrazoHoras = 4 }
            );
        }
    }
}