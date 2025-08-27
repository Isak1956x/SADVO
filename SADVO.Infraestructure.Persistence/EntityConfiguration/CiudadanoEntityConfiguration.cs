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
    public class CiudadanoEntityConfiguration : IEntityTypeConfiguration<Ciudadano>
    {
        public void Configure(EntityTypeBuilder<Ciudadano> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("Ciudadano");
            #endregion
            #region Property Configuration
            builder.Property(a => a.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Apellido).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(200);
            builder.Property(a => a.DocumentoIdentidad).IsRequired().HasMaxLength(50);
            builder.Property(a => a.EstaActivo).IsRequired().HasDefaultValue(true);
            #endregion
           
        }
    }
}

