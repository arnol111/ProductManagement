using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Product.Queries.GetByCode
{
    public class GetProductByCodeQuery : IRequest<ProductDto>
    {
        public string ProductCode { get; set; } 
    }
}
