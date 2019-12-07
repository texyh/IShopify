using FluentValidation;
using IShopify.Core.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.DomainServices.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);

            RuleFor(x => x.DepartmentId).NotEmpty();

            RuleFor(x => x.Description).NotEmpty().MinimumLength(3);
        }
    }
}
