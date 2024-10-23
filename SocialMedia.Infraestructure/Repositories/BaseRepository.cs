using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        protected  readonly DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
           await  _entities.AddAsync(entity);
           
        }

    
        public async Task DeleteAsync(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            
        }

   

        public IEnumerable<T> GetAll()
        {
            return  _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            var Get = await _entities.FindAsync(id);
            return Get!;
        }

        public void  Update(T entity)
        {
            _entities.Update(entity);
            
        }


    }
}