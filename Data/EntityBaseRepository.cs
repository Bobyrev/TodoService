using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TodoApi.Data
{
    using Abstract;

    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        protected TodoContext _context;

        public EntityBaseRepository(TodoContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() 
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetSingleAsync(long id) 
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity ?? throw new ArgumentException($"Entity with type = {typeof(T).Name} and id = {id} does not exist");
        }

        public async virtual Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity, bool commit = true) 
        {
            if (entity is null)
                throw new ArgumentNullException($"Input entity is null");

            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            if (commit)
            {
                await CommitAsync();
            }
        }

        public virtual async Task AddAsync(T entity, bool commit = true)
        {
            if (entity is null)
                throw new ArgumentNullException($"Input entity is null");

            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
            if (commit)
            {
               await CommitAsync();
            }
        }

        public virtual async Task DeleteAsync(T entity, bool commit = true)
        {
            if (entity is null)
                throw new ArgumentNullException($"Input entity is null");

            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
            if (commit)
            {
                await CommitAsync();
            }
        }

        public virtual bool IsExist(long id) 
        {
            return _context.Set<T>().Any(x => x.Id == id);
        }
    }
}
