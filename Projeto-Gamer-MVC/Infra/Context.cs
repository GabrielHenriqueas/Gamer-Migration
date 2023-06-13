using Microsoft.EntityFrameworkCore;
using Projeto_Gamer_MVC.Models;

namespace Projeto_Gamer_MVC.Infra
{
    public class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = NOTE06-S14; initial catalog = gamerTarde; User Id=sa; pwd=Senai@134; TrustServerCertificate = true");
            }
        }

        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Equipe> Equipe { get; set; }

    }
}