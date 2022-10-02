using ApplicationCore.Exceptions;
using ApplicationCore.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Product.Queries.GetByCode
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IMapper _mapper;


        public GetProductByCodeQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {

            var product = await _applicationDbContext
                .Products               
                .Where(x => x.ProductCode == request.ProductCode && x.State == "Activo")
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if(product == null)
            {
                throw new NotFoundException($"{nameof(product)}: The product with the code: {request.ProductCode} doesn't exists");
            }
                
            return _mapper.Map<ProductDto>(product);
        }
    }
}
