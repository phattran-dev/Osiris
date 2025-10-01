using Microsoft.EntityFrameworkCore;

namespace Database.Core.Interfaces
{
    public interface IRepository<TDbContext, TEntity>
            where TDbContext : DbContext
            where TEntity : class
    {
        /// <summary>
        /// Directly to the Entities of this repository. Use for complex and easier to optimization query.
        /// </summary>
        DbSet<TEntity> Entities();
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task HardDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RestoreSoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task SoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task RestoreSoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
