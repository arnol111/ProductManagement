using ApplicationCore.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Product.Queries.GetProductWithPagination
{
    public class GetProductWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
    {
        public List<Filter> Filters { get; set; }
        public int PageNumber { get; } = 1;
        public int PageSize { get; } = 10;
    }
}
