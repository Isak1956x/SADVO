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
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            #region Basic Configuration
            builder.HasKey(u => u.Id);
            builder.ToTable("Users");
            #endregion

            #region Property Configuration
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(100);
            builder.Property(u => u.ContrasenaHash).IsRequired().HasMaxLength(64);
            builder.Property(u => u.Rol).IsRequired();
            builder.Property(u => u.EstaActivo).IsRequired().HasDefaultValue(true);
            #endregion

            #region Relationships

            builder.HasOne(u => u.DirigentePolitico)
                   .WithOne(d => d.Usuario)
                   .HasForeignKey<DirigentePolitico>(d => d.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

        }
    }
} 
