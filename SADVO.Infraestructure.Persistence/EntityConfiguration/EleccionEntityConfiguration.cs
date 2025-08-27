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
    public class EleccionEntityConfiguration : IEntityTypeConfiguration<Eleccion>
    {
        public void Configure(EntityTypeBuilder<Eleccion> builder)
        {
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("Eleccion");
            #endregion
            #region Property Configuration
            builder.Property(a => a.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(a => a.EstaFinalizada).IsRequired().HasDefaultValue(false);
            builder.Property(a => a.Fecha).IsRequired();

            #endregion


        }
    }
}
