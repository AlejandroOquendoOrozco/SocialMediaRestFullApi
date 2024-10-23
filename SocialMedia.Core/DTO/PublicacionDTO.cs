using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTO
{
    public class PublicacionDTO
    {
        /// <summary>
        /// Autigenerated Id for post entity
        /// </summary>
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; } = null!;

        public string? Imagen { get; set; }
    }
}