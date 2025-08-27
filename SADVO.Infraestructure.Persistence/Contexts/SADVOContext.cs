using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Domain.Entities;
using SADVO.Infraestructure.Persistence.EntityConfiguration;

namespace SADVO.Infraestructure.Persistence.Contexts
{
    public class SADVOContext : DbContext
    {
        public SADVOContext(DbContextOptions<SADVOContext> options) : base(options)
        {
        }
        public DbSet<AlianzaPolitica> AlianzasPoliticas { get; set; }
        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
        public DbSet<PuestoElectivo> PuestosElectivos { get; set; }
        public DbSet<DirigentePolitico> DirigentesPoliticos { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<AsignacionPuesto> AsignacionesPuestos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ciudadano> Ciudadanos { get; set; }
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<Voto> Votos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
     
       
    }
  
}
