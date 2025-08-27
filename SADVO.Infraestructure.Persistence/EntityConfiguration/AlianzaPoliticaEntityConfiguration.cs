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
    public class AlianzaPoliticaEntityConfiguration : IEntityTypeConfiguration<AlianzaPolitica>
    {
        public void Configure(EntityTypeBuilder<AlianzaPolitica> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("AlianzasPoliticas");

            #endregion
            #region Property Configuration
            builder.Property(a => a.Estado)
                           .IsRequired()
                           .HasDefaultValue(1);
            builder.Property(a => a.FechaSolicitud)
                .HasDefaultValueSql("GETDATE()");
            #endregion
            #region Relationships
            builder.HasOne(a => a.PartidoSolicitante)
               .WithMany(p => p.AlianzasEnviadas)
               .HasForeignKey(a => a.PartidoSolicitanteId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PartidoDestino)
          .WithMany(p => p.AlianzasRecibidas)
          .HasForeignKey(a => a.PartidoDestinoId)
          .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
