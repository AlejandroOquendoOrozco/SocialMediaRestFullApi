﻿using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities;

public partial class Usuario:BaseEntity
{
   

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; } = new List<Comentario>();

    public virtual ICollection<Publicacion> Publicacions { get; } = new List<Publicacion>();
}
