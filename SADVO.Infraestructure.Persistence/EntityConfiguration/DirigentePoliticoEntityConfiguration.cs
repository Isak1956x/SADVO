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
    public class DirigentePoliticoEntityConfiguration : IEntityTypeConfiguration<DirigentePolitico>
    {
        public void Configure(EntityTypeBuilder<DirigentePolitico> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("DirigentePolitico");
            #endregion

            #region Relationships
            builder.HasOne(d => d.Usuario)
             .WithOne(u => u.DirigentePolitico)
             .HasForeignKey<DirigentePolitico>(d => d.UsuarioId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.PartidoPolitico)
                   .WithMany()
                   .HasForeignKey(d => d.PartidoPoliticoId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}

