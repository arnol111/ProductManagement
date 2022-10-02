using ApplicationCore.Exceptions;
using ApplicationCore.Services;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateProductCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Products()
            {
                Description = request.Description,
                ProductCode = request.ProductCode,
                State = request.State,
                ManufacturingDate = request.ManufacturingDate,
                ExpirationDate = request.ExpirationDate,
                ProviderCode = request.ProviderCode,
                ProviderDescription = request.ProviderDescription,
                ProviderPhone = request.ProviderPhone
            };

            var dateComparer = DateTime.Compare(product.ManufacturingDate, product.ExpirationDate);

            if (dateComparer >= 0)
            {
                throw new InvalidDateProductException("The Manufacturing date can't equal or more than the expiration date");
            }

            product.AddDomainEvent(new CreateProductsEvent(product));

            _applicationDbContext.Products.Add(product);

            await _applicationDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false); 

            return product.Id;
        }
    }
}

