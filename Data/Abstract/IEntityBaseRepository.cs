using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Data.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get single item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(long id);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, bool commit = true);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        Task AddAsync(T entity, bool commit = true);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool commit = true);

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// Check for exist entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsExist(long id);
    }
}
