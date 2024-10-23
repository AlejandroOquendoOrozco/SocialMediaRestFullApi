using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PublicacionRepository : BaseRepository<Publicacion>, IPublicacionRepository
    {
        
        public PublicacionRepository(SocialMediaContext context):base(context){
        }
        public async Task<IEnumerable<Publicacion>> GetPublicacionById(int id)
        {
            return await _entities.Where(x=>x.IdUsuario==id).ToListAsync();
        }
    }
}