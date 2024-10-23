using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Infraestructure.Data.Configurations;
namespace SocialMedia.Infraestructure.Data;

public partial class SocialMediaContext : DbContext
{
    public SocialMediaContext()
    {
    }

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Publicacion> Publicacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Seguridad> Seguridades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=DESKTOP-UC3ESN4\\SQLEXPRESS02;Database=SocialMedia;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       /* modelBuilder.ApplyConfiguration(new ComentarioConfigurations());

        modelBuilder.ApplyConfiguration(new PublicacionConfigurations());

        modelBuilder.ApplyConfiguration(new UsuarioConfigurations());*/

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        
    }

  
}
