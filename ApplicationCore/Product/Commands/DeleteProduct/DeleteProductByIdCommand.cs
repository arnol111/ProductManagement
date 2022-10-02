using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Product.Commands.DeleteProduct
{
    public class DeleteProductByIdCommand : IRequest
    {
        public int ProductId { get; set; }
    }
}
