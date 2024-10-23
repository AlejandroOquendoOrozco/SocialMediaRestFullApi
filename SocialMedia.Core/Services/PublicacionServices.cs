
using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntitys;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Options;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Services
{
    public class PublicacionServices : IPublicacionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationsOptions _paginationsOptions;
        public PublicacionServices(IUnitOfWork unitOfWork,IOptions<PaginationsOptions> paginationsOptions)
        {

            _unitOfWork = unitOfWork;
            _paginationsOptions = paginationsOptions.Value;
        }

        public async Task AddPublicacion(Publicacion publicacion)
        {

            var User = await _unitOfWork.UsuariosRepository.GetById(publicacion.IdUsuario);
            if (User == null)
            {

                throw new BusinessExeptions("Usuario No Existe");
            }
            var UsuarioPublicacion = await _unitOfWork.PublicacionRepository.GetPublicacionById(publicacion.IdUsuario);
            if (UsuarioPublicacion.Count() < 10)
            {
                var UltimoPost = UsuarioPublicacion.OrderByDescending(x => x.Fecha).FirstOrDefault();
                if (UltimoPost != null && (DateTime.Now - UltimoPost.Fecha).TotalDays < 7)
                {
                    throw new BusinessExeptions("NO PUEDES PUBLICAR");
                }
            }
            if (publicacion.Descripcion.Contains("Sexo"))
            {
                throw new BusinessExeptions("CONTIENE PALABRAS BULGARES");
            }
            await _unitOfWork.PublicacionRepository.Add(publicacion);
            await _unitOfWork.SaveChangeAsync();

        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PublicacionRepository.DeleteAsync(id);
            return true;
        }

        public PageList<Publicacion> GetPostsAsync(PublicacionQueryFilter publicacion)
        {
            publicacion.PageNumber = publicacion.PageNumber==0?_paginationsOptions.DefaultPageNumber:publicacion.PageNumber;
            publicacion.PageSize = publicacion.PageSize == 0 ? _paginationsOptions.DefaultPageSize : publicacion.PageSize;
            var post = _unitOfWork.PublicacionRepository.GetAll();
            if(publicacion.UserId != null){
                 post = post.Where(x => x.IdUsuario.Equals(publicacion.UserId));
            }
            if(publicacion.Fecha != null){
                 post = post.Where(x => x.Fecha.ToShortDateString().Equals(publicacion.Fecha?.ToShortDateString()));
            }
            if(publicacion.Descripcion != null){
                post = post.Where(x => x.Descripcion.ToLower().Contains(publicacion.Descripcion));
            }

            var PagePost=PageList<Publicacion>.Create(post,publicacion.PageNumber, publicacion.PageSize);
    
            return PagePost;
        }

        public async Task<Publicacion> GetPublicacionAsync(int id)
        {
            return await _unitOfWork.PublicacionRepository.GetById(id);
        }

        public async Task<bool> UpdatePost(Publicacion post)
        {
            _unitOfWork.PublicacionRepository.Update(post);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }


    }


}