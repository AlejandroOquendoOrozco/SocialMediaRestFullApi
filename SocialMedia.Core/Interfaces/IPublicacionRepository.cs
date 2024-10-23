using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPublicacionRepository:IRepository<Publicacion>
    {
        Task<IEnumerable<Publicacion>> GetPublicacionById(int id);
    }
}