using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SADVO.Core.Domain.Entities;

namespace SADVO.Infraestructure.Persistence.EntityConfiguration
{
    public class AsignacionPuestoEntityConfiguration : IEntityTypeConfiguration<AsignacionPuesto>
    {
        public void Configure(EntityTypeBuilder<AsignacionPuesto> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("AsignacionPuesto");
            #endregion

            #region Relationships
            builder.HasOne(ap => ap.Candidato)
                .WithMany(c => c.Asignaciones)
                .HasForeignKey(ap => ap.CandidatoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ap => ap.PuestoElectivo)
                   .WithMany(p => p.Asignaciones)
                   .HasForeignKey(ap => ap.PuestoElectivoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ap => ap.PartidoPolitico)
                   .WithMany()
                   .HasForeignKey(ap => ap.PartidoPoliticoId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
