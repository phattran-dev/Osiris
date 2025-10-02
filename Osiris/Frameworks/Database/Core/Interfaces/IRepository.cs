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

        /// <summary>
        /// Add an entity record without requiring AuthorAudited
        /// </summary>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add an entity record with requiring AuthorAudited
        /// </summary>
        Task AddAsync<T>(TEntity entity, T createdBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Add multiple entity records without requiring AuthorAudited
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple entity records with requiring AuthorAudited
        /// </summary>
        Task AddRangeAsync<T>(IEnumerable<TEntity> entities, T createdBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Update an entity record without requiring AuthorAudited
        /// </summary>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an entity record with requiring AuthorAudited
        /// </summary>
        Task UpdateAsync<T>(TEntity entity, T updatedBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Update multiple entity records without requiring AuthorAudited
        /// </summary>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update multiple entity records with requiring AuthorAudited
        /// </summary>
        Task UpdateRangeAsync<T>(IEnumerable<TEntity> entities, T updatedBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Hard delete an entity record without requiring AuthorAudited. This will permanently remove the record from the database.
        /// </summary>
        Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Hard delete multiple entity records without requiring AuthorAudited. This will permanently remove the records from the database.
        /// </summary>
        Task HardDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft delete an entity record without requiring AuthorAudited. This will mark the record as deleted without permanently removing it from the database.
        /// </summary>
        Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft delete an entity record with requiring AuthorAudited. This will mark the record as deleted without permanently removing it from the database.
        /// </summary>
        Task SoftDeleteAsycn<T>(TEntity entity, T deletedBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Restore a soft-deleted entity record without requiring AuthorAudited. This will unmark the record as deleted, making it active again.
        /// </summary>
        Task RestoreSoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Restore a soft-deleted entity record with requiring AuthorAudited. This will unmark the record as deleted, making it active again.
        /// </summary>
        Task RestoreSoftDeleteAsync<T>(TEntity entity, T restoredBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Soft delete multiple entity records without requiring AuthorAudited. This will mark the records as deleted without permanently removing them from the database.
        /// </summary>
        Task SoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft delete multiple entity records with requiring AuthorAudited. This will mark the records as deleted without permanently removing them from the database.
        /// </summary>
        Task SoftDeleteRangeAsync<T>(IEnumerable<TEntity> entities, T deletedBy, CancellationToken cancellationToken = default) where T : struct;

        /// <summary>
        /// Restore multiple soft-deleted entity records without requiring AuthorAudited. This will unmark the records as deleted, making them active again.
        /// </summary>
        Task RestoreSoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Resoe multiple soft-deleted entity records with requiring AuthorAudited. This will unmark the records as deleted, making them active again.
        /// </summary>
        Task RestoreSoftDeleteRangeAsync<T>(IEnumerable<TEntity> entities, T restoredBy, CancellationToken cancellationToken = default) where T : struct;
    }
}
