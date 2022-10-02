using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Product
{
    public class ProductDto
    {
        [MinLength(4)]
        [Required]
        public string ProductCode { set; get; }

        [MaxLength(500)]
        [Required]
        public string Description { set; get; }

        [Required]
        public string State { set; get; }

        public DateTime ManufacturingDate { set; get; }

        public DateTime ExpirationDate { set; get; }

        public string ProviderCode { get; set; }

        public string ProviderDescription { get; set; }

        public double ProviderPhone { set; get; }

        public int ProductId { get; set; }
    }
}
