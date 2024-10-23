
using SocialMedia.Core.CustomEntitys;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Services
{
    public interface IPublicacionService
    {
        Task AddPublicacion(Publicacion publicacion);
        PageList<Publicacion> GetPostsAsync(PublicacionQueryFilter publicacion);
        Task<Publicacion> GetPublicacionAsync(int id);
        

        Task<bool> UpdatePost(Publicacion post);
        Task<bool> DeletePost(int id);
    }
}