using CarControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarControl.Infrastructure
{
    public class CarControlContext : DbContext
    {
        public CarControlContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Veiculo>().HasKey(pp => new { pp.IdVeiculo });
            modelBuilder.Entity<Vaga>().HasKey(pp => new { pp.IdVaga });
            modelBuilder.Entity<Operacao>().HasKey(pp => new { pp.IdTpOperacao });
            modelBuilder.Entity<Movimento>().HasKey(pp => new { pp.IdMovimento, pp.IdVeiculo});


            modelBuilder.Entity<Movimento>().HasOne(s => s.IdTpOperacao);
            modelBuilder.Entity<Movimento>().HasOne(s => s.IdVaga);


        }

    



    }
}
