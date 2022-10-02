using ApplicationCore.Exceptions;
using ApplicationCore.Product.Queries;
using ApplicationCore.Services;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateProductCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var alll = await _applicationDbContext.Products.ToListAsync().ConfigureAwait(false);
            //looking product by id
            var product = await _applicationDbContext.Products
                .Where(x => x.Id == request.ProductToUpdate.ProductId && x.State == "Activo")
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (product == null)
            {
                throw new NotFoundException($"{nameof(product)}: The product with the Id: {request.ProductToUpdate.ProductId} doesn't exists");
            }

            //Validations

            var dateComparer = DateTime.Compare(request.ProductToUpdate.ManufacturingDate, request.ProductToUpdate.ExpirationDate);

            if (dateComparer >= 0)
            {
                throw new InvalidDateProductException("The Manufacturing date can't equal or more than the expiration date");
            }

            // Update
            product.Description = request.ProductToUpdate.Description;
            product.ProviderDescription = request.ProductToUpdate.ProviderDescription;
            product.ProviderPhone = request.ProductToUpdate.ProviderPhone;
            product.ManufacturingDate= request.ProductToUpdate.ManufacturingDate;
            product.ExpirationDate = request.ProductToUpdate.ExpirationDate;
            product.ProductCode = request.ProductToUpdate.ProductCode;
            product.State = request.ProductToUpdate.State;
            product.ProviderCode = request.ProductToUpdate.ProviderCode;


            product.AddDomainEvent(new UpdateProductsEvent(product));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
