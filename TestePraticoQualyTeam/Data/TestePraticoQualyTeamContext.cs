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

        public DbSet<TestePraticoQualyTeam.Model.Categoria> Categorias { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasIndex(c => c.codigo).IsUnique();
                entity.HasOne(d => d.processo);
                entity.HasOne(d => d.categoria);
                entity.ToTable("Documento");
                ;
                
            });
               

            modelBuilder.Entity<Processo>().ToTable("Processo")
                .HasMany<Categoria>(d => d.categorias)
                .WithMany(d => d.processos)
                .UsingEntity(j => j.ToTable("ProcessosCategorias")); ;
                    

            modelBuilder.Entity<Categoria>().ToTable("Categoria");



        }

     


    }
}
