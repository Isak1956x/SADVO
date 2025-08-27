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
    public class CandidatoEntityConfiguration : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {

            #region Basic Configuration
            builder.HasKey(u => u.Id);
            builder.ToTable("Candidato");
            #endregion

            #region Property Configuration
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(200);
            builder.Property(u => u.FotoUrl).IsRequired().HasMaxLength(200);
            builder.Property(u => u.EstaActivo).IsRequired().HasDefaultValue(true);
            #endregion

            #region Relationship Configuration
            builder.HasOne(c => c.PartidoPolitico)
                   .WithMany(pp => pp.Candidatos)
                   .HasForeignKey(c => c.PartidoPoliticoId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
