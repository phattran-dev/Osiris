using Microsoft.EntityFrameworkCore;

namespace Database.Core.Interfaces
{
    public interface IBaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
         where TEntity : class
    {
    }
}
