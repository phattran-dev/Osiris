using Microsoft.EntityFrameworkCore;

namespace Database.Core.Interfaces
{
    public interface IUnitOfWork<TDbContext> : IDisposable, IAsyncDisposable
        where TDbContext : DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
