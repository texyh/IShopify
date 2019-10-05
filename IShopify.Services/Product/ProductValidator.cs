using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using models = IShopify.Core.Products.Models;
using static IShopify.DomainServices.Validation.ValidatorConstants;

namespace IShopify.DomainServices.Product
{
    public class ProductValidator : AbstractValidator<models.Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull();

            RuleFor(x => x.Description).NotNull();

            RuleFor(x => x.Price).NotEqual(MinimumProductPrice);
        }
    }
}
