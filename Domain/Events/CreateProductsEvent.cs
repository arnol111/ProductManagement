using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class CreateProductsEvent : BaseEvents
    {
        public Products Product { get; }

        public CreateProductsEvent(Products products)
        {
            Product = products;
        }

    }
}
