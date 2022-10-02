using ApplicationCore.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Product.Queries.GetProductWithPagination
{
    public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<ProductDto>>
    {
        public Task<PaginatedList<ProductDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
