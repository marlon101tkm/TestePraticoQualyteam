using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestePraticoQualyTeam.Model;

namespace TestePraticoQualyTeam.Data
{
    public class TestePraticoQualyTeamContext : DbContext
    {
        public TestePraticoQualyTeamContext (DbContextOptions<TestePraticoQualyTeamContext> options)
            : base(options)
        {
        }

       public DbSet<TestePraticoQualyTeam.Model.Documento> Documentos { get; set; }

       public DbSet<TestePraticoQualyTeam.Model.Processo> Processos { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasIndex(c => c.codigo).IsUnique();
                entity.ToTable("Documento").HasOne(d => d.processo);
            });
               

            modelBuilder.Entity<Processo>().ToTable("Processo");
            
        }
      

    }
}
