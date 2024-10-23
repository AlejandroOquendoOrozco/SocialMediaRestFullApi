
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IPublicacionRepository PublicacionRepository { get; }

        IRepository<Usuario> UsuariosRepository { get; }

        IRepository<Comentario> ComentariosRepository { get; }

        ISeguridadRepository SeguridadRepository { get; }

        void SaveChange();

        Task SaveChangeAsync();
    }
}