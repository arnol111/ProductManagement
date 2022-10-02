using ApplicationCore.Exceptions;
using ApplicationCore.Services;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Product.Commands.HardDeleteProduct
{
    public class HardDeleteProductByIdCommandHandler : IRequestHandler<HardDeleteProductByCodeCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public HardDeleteProductByIdCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(HardDeleteProductByCodeCommand request, CancellationToken cancellationToken)
        {
            //looking product by id
            var product = await _applicationDbContext.Products
                .Where(x => x.ProductCode == request.Code)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (product == null)
            {
                throw new NotFoundException($"{nameof(product)}: The product with the code: {request.Code} doesn't exists");
            }

            _applicationDbContext.Products.Remove(product);

            product.AddDomainEvent(new DeleteProductsEvent(product));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
