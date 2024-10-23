using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class SeguridadRepository : BaseRepository<Seguridad>,ISeguridadRepository
    {
        public SeguridadRepository(SocialMediaContext context) : base(context)
        {
        }

        public async Task<Seguridad?>GetLoginByCredencial(UserLogin userLogin)
        {
            var UserLogin= await _entities.FirstOrDefaultAsync(x => x.User == userLogin.User && x.Password == userLogin.Password);
            return UserLogin;

        }
    }
}
