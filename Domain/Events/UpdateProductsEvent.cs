using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class UpdateProductsEvent : BaseEvents
    {
        public Products Product { get; }

        public UpdateProductsEvent(Products products)
        {
            Product = products;
        }

    }
}
