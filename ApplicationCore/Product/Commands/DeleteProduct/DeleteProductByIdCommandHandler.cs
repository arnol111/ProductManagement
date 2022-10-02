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

namespace ApplicationCore.Product.Commands.DeleteProduct
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteProductByIdCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {

            //looking product by id
            var product = await _applicationDbContext.Products
                .Where(x => x.Id == request.ProductId && x.State == "Activo")
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if(product== null)
            {
                throw new NotFoundException($"{nameof(product)}: The product with the Id: {request.ProductId} doesn't exists");
            }

            product.AddDomainEvent(new DeleteProductsEvent(product));

            // Logical deleted
            product.State = "Inactivo";
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
