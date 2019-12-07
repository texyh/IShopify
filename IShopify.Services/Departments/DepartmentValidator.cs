using FluentValidation;
using IShopify.Core.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.DomainServices.Departments
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3);

            RuleFor(x => x.Description).NotNull().MinimumLength(3);
        }
    }
}
