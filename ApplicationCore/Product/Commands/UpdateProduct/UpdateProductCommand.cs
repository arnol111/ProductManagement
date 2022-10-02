using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        [Required]
        public ProductDto ProductToUpdate { get; set; }        

    }
}
