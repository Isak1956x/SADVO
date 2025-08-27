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
    public class VotoEntityConfiguration : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {

            #region Basic Configuration
            builder.HasKey(u => u.Id);
            builder.ToTable("Voto");
            #endregion

            #region Property Configuration
            builder.Property(u => u.FechaHora).IsRequired().HasDefaultValueSql("GETDATE()");

            #endregion

            #region Relationships Configuration
            builder.HasOne(v => v.Ciudadano)
             .WithMany(c => c.Votos)
             .HasForeignKey(v => v.CiudadanoId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Eleccion)
                   .WithMany(e => e.Votos)
                   .HasForeignKey(v => v.EleccionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.PuestoElectivo)
                   .WithMany()
                   .HasForeignKey(v => v.PuestoElectivoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Candidato)
                   .WithMany(c => c.Votos)
                   .HasForeignKey(v => v.CandidatoId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }

    }
}

