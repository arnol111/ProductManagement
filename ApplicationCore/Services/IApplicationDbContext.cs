using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IApplicationDbContext
    {
        DbSet<Products> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
