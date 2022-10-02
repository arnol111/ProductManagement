using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Services;
using Domain.Entities;
using Domain.Identity;
using IdentityServer4.EntityFramework.Options;
using Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IMediator mediator) : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
        }

        public DbSet<Products> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
