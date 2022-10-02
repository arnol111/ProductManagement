using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        [Required]
        public string ProductCode { set; get; }

        [Required]
        public string Description { set; get; }

        [Required]
        public string State { set; get; }

        public DateTime ManufacturingDate { set; get; }

        public DateTime ExpirationDate { set; get; }

        public string ProviderCode { get; set; }

        public string ProviderDescription { get; set; }

        public int ProviderPhone { set; get; }

    }
}
