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
    public class PartidoPoliticoEntityConfiguration : IEntityTypeConfiguration<PartidoPolitico>
    {
        public void Configure(EntityTypeBuilder<PartidoPolitico> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("PartidoPolitico");
            #endregion
            #region Property Configuration
            builder.Property(a => a.Nombre).IsRequired().HasMaxLength(2000);
            builder.Property(a => a.Siglas).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Descripcion).HasMaxLength(2000);
            builder.Property(a => a.EstaActivo).IsRequired().HasDefaultValue(true);
            #endregion

        }
    }
}