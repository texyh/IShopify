using FluentValidation;
using IShopify.Core.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using models = IShopify.Core.Orders.Models;

namespace IShopify.DomainServices.Order
{
    public class OrderItemValidator : AbstractValidator<models.Order>
    {
        
    }
}
