using Database.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Database.Core
{
    public abstract class BaseRelationDbEntityTypeConfiguration<TEntity> : IBaseEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Naming convention: Pluralize table name by adding 's' at the end
            builder.ToTable($"{typeof(TEntity).Name}s");

            #region Default Setting Primary Key For Entity
            var hasIdColumn = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (hasIdColumn != null)
            {
                builder.HasKey("Id");
            }
            #endregion Default Setting Primary Key For Entity
        }
    }
}
