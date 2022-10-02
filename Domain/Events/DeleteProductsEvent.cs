using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class DeleteProductsEvent : BaseEvents
    {
        public Products Product { get; }

        public DeleteProductsEvent(Products products)
        {
            Product = products;
        }

    }
}
