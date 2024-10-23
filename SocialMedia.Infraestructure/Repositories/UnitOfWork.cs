
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private  readonly IPublicacionRepository _postRepository;
        private readonly IRepository<Usuario> _userRepository;
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly IRepository<Comentario> _commentRepository;
        private readonly SocialMediaContext _context;
       
        
        public UnitOfWork(SocialMediaContext context,IPublicacionRepository postRepository,IRepository<Usuario> userRepository,IRepository<Comentario> commentRepository,ISeguridadRepository seguridadRepository){
            _context = context;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _seguridadRepository = seguridadRepository;
        }
        public IPublicacionRepository PublicacionRepository => _postRepository?? new PublicacionRepository(_context);

        public IRepository<Usuario> UsuariosRepository =>_userRepository?? new BaseRepository<Usuario>(_context);

        public IRepository<Comentario> ComentariosRepository => _commentRepository?? new BaseRepository<Comentario>(_context);

        public ISeguridadRepository SeguridadRepository => _seguridadRepository?? new SeguridadRepository(_context);

        public void Dispose()
        {
            if(_context!=null){
                _context.Dispose();
            }
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync() ;
        }
    }
}