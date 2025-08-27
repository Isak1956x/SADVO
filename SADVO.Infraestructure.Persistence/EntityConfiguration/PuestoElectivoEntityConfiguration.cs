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
    public class PuestoElectivoEntityConfiguration : IEntityTypeConfiguration<PuestoElectivo>
    {
        public void Configure(EntityTypeBuilder<PuestoElectivo> builder)
        {

            #region Basic Configuration
            builder.HasKey(u => u.Id);
            builder.ToTable("PuestoElectivo");
            #endregion

            #region Property Configuration
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Descripcion).IsRequired().HasMaxLength(200);
            builder.Property(u => u.EstaActivo).IsRequired().HasDefaultValue(true);
            #endregion

        }
    }
   
}
