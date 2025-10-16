using Database.Core.Interfaces;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Core
{
    public class Repository<TDbContext, TEntity> : IRepository<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly TDbContext _dbContext;
        protected Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<TEntity> Entities() => _dbContext.Set<TEntity>();

        public IQueryable<TEntity> QueryAvailableEntities()
        {
            if (typeof(ISoftDeleteAudited).IsAssignableFrom(typeof(TEntity)))
            {
                return _dbContext.Set<TEntity>().Where(e => !(e as ISoftDeleteAudited).IsDeleted);
            }
            return _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ProcessDateAudited(entity, false);
            await Entities().AddAsync(entity, cancellationToken);
        }

        public async Task AddAsync<T>(TEntity entity, T createdBy, CancellationToken cancellationToken = default) where T : struct
        {
            ProcessDateAudited(entity, false);
            ProcessAuthorAudited(entity, createdBy, false);
            await Entities().AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                ProcessDateAudited(entity, false);
            await Entities().AddRangeAsync(entities, cancellationToken);
        }

        public async Task AddRangeAsync<T>(IEnumerable<TEntity> entities, T createdBy, CancellationToken cancellationToken = default) where T : struct
        {
            foreach (var entity in entities)
            {
                ProcessDateAudited(entity, false);
                ProcessAuthorAudited(entity, createdBy, false);
            }
            await Entities().AddRangeAsync(entities, cancellationToken);
        }

        public Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Entities().Remove(entity);
            return Task.CompletedTask;
        }

        public Task HardDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Entities().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ProcessDateAudited(entity, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync<T>(TEntity entity, T updatedBy, CancellationToken cancellationToken = default) where T : struct
        {
            ProcessDateAudited(entity, true);
            ProcessAuthorAudited(entity, updatedBy, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }
        
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                ProcessDateAudited(entity, true);
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync<T>(IEnumerable<TEntity> entities, T updatedBy, CancellationToken cancellationToken = default) where T : struct
        {
            foreach (var entity in entities)
            {
                ProcessDateAudited(entity, true);
                ProcessAuthorAudited(entity, updatedBy, true);
            }
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ProcessSoftDeleteAudited(entity, true);
            ProcessDateAudited(entity, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }

        public Task SoftDeleteAsycn<T>(TEntity entity, T deletedBy, CancellationToken cancellationToken = default) where T : struct
        {
            ProcessSoftDeleteAudited(entity, true, deletedBy);
            ProcessDateAudited(entity, true);
            ProcessAuthorAudited(entity, deletedBy, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }

        public Task RestoreSoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ProcessSoftDeleteAudited(entity, false);
            ProcessDateAudited(entity, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }

        public Task RestoreSoftDeleteAsync<T>(TEntity entity, T restoredBy, CancellationToken cancellationToken = default) where T : struct
        {
            ProcessSoftDeleteAudited(entity, false, restoredBy);
            ProcessDateAudited(entity, true);
            ProcessAuthorAudited(entity, restoredBy, true);
            Entities().Update(entity);
            return Task.CompletedTask;
        }

        public Task SoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                ProcessSoftDeleteAudited(entity, true);
                ProcessDateAudited(entity, true);
            }
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task SoftDeleteRangeAsync<T>(IEnumerable<TEntity> entities, T deletedBy, CancellationToken cancellationToken = default) where T : struct
        {
            foreach (var entity in entities)
            {
                ProcessSoftDeleteAudited(entity, true, deletedBy);
                ProcessDateAudited(entity, true);
                ProcessAuthorAudited(entity, deletedBy, true);
            }
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task RestoreSoftDeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                ProcessSoftDeleteAudited(entity, false);
                ProcessDateAudited(entity, true);
            }
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task RestoreSoftDeleteRangeAsync<T>(IEnumerable<TEntity> entities, T restoredBy, CancellationToken cancellationToken = default) where T : struct
        {
            foreach (var entity in entities)
            {
                ProcessSoftDeleteAudited(entity, false, restoredBy);
                ProcessDateAudited(entity, true);
                ProcessAuthorAudited(entity, restoredBy, true);
            }
            Entities().UpdateRange(entities);
            return Task.CompletedTask;
        }

        #region Protected Methods
        protected void ProcessDateAudited(TEntity entity, bool isUpdate)
        {
            if (!(entity is IDateAudited dateAudited))
                return;

            if (isUpdate)
            {
                dateAudited.UpdatedDate = DateTime.UtcNow;
            }
            else // isCreate
            {
                dateAudited.CreatedDate = DateTime.UtcNow;
                dateAudited.UpdatedDate = DateTime.UtcNow;
            }
        }

        protected void ProcessAuthorAudited<T>(TEntity entity, T authorId, bool isUpdate)
        {
            if (!(entity is IAuthorAudited<T> authorAudited))
                return;

            if (isUpdate)
            {
                authorAudited.UpdatedBy = authorId;
            }
            else // isCreate
            {
                authorAudited.CreatedBy = authorId;
                authorAudited.UpdatedBy = authorId;
            }
        }

        /// <summary>
        /// Process soft delete audited without Author
        /// </summary>
        protected void ProcessSoftDeleteAudited(TEntity entity, bool isDelete)
        {
            if (!(entity is ISoftDeleteAudited softDeleteAudited))
                return;

            softDeleteAudited.IsDeleted = isDelete;
            if (isDelete)
            {
                softDeleteAudited.DeletedDate = DateTime.UtcNow;
            }
            else
            {
                softDeleteAudited.DeletedDate = null;
            }
        }

        /// <summary>
        /// Process soft delete audited with Author
        /// </summary>
        protected void ProcessSoftDeleteAudited<T>(TEntity entity, bool isDelete, T userId) where T : struct
        {
            if (!(entity is ISoftDeleteAudited softDeleteAudited))
                return;
            softDeleteAudited.IsDeleted = isDelete;
            if (isDelete)
            {
                softDeleteAudited.DeletedDate = DateTime.UtcNow;
            }
            else
            {
                softDeleteAudited.DeletedDate = null;
            }
        }
        #endregion
    }
}
