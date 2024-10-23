using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    internal class SeguridadConfigurations : IEntityTypeConfiguration<Seguridad>
    {
        public void Configure(EntityTypeBuilder<Seguridad> entity)
        {
            // Mapea la propiedad Id a la columna idpublicacion
            entity.Property(e => e.Id)
                .HasColumnName("IdSeguridad");

            // Especifica la clave primaria después de mapear la columna
            entity.HasKey(e => e.Id);

            entity.ToTable("Seguridad");

            entity.Property(e => e.User)
                .IsRequired()
                .HasColumnName("Usuario")
                 .IsUnicode(false)
                .HasMaxLength(50);
            entity.Property(e => e.UserName)
                .HasColumnName("NombreUsuario")
                .IsUnicode(false)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasColumnName("Contraseña")
                .IsRequired()
                .IsUnicode (false)
                .HasMaxLength(200);
            entity.Property(e => e.Role)
                .HasColumnName("Rol")
                .HasMaxLength(15)
                .IsRequired()
                .HasConversion(x => x.ToString(), x => (RolType)Enum.Parse(typeof(RolType), x));
               

        }
    }
}
