using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using models =  IShopify.Core.Customer.Models;

namespace IShopify.DomainServices.Customer
{
    class CustomerValidator : AbstractValidator<models.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotNull();

            RuleFor(x => x.Email).NotNull();
        }
    }
}
