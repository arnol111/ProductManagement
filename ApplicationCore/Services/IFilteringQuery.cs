using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IFilteringQuery<T>
    {
        IQueryable<T> AddFiltersToQuery(IQueryable<T> query, Filter filter);

        Task<PaginatedList<T>> PaginationAsync(IQueryable<T> source, int pageNumber, int pageSize);

    }
}
