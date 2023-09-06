using CarControl.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarControl.Infrastructure
{
    public class CarControlContext : IdentityDbContext<ApplicationUser>
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
            modelBuilder.Entity<Movimento>().HasKey(pp => new { pp.IdMovimento});
            modelBuilder.Entity<ApplicationUser>().HasKey(pp => new { pp.Id});

            modelBuilder.Entity<Movimento>().Property(e => e.IdMovimento).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movimento>().HasOne(m => m.TpOperacao).WithMany(o => o.Movimentos).HasForeignKey(m => m.IdTpOperacao);
            modelBuilder.Entity<Movimento>().HasOne(m => m.Veiculo).WithMany(o => o.Movimentos).HasForeignKey(m => m.IdVeiculo);
            modelBuilder.Entity<Movimento>().HasOne(s => s.Vaga).WithMany(o => o.Movimentos).HasForeignKey(m => m.IdVaga);
            modelBuilder.Entity<Movimento>().HasIndex(e => e.IdTpOperacao).IsUnique(false);
            modelBuilder.Entity<Movimento>().HasIndex(e => e.IdVaga).IsUnique(false);
            modelBuilder.Entity<Movimento>().HasIndex(e => e.IdVeiculo).IsUnique(false);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });


        }

    }
}
