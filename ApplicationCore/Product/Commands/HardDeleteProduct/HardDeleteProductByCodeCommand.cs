using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Product.Commands.HardDeleteProduct
{
    public class HardDeleteProductByCodeCommand : IRequest
    {
        public string Code { get; set; }
    }
}
