using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface ISeguridadRepository:IRepository<Seguridad>
    {
        Task<Seguridad?> GetLoginByCredencial(UserLogin userLogin);
    }
}
