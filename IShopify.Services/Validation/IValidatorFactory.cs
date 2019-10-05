using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Validation
{
    public interface IValidatorFactory
    {
        IValidator<TModel> GetValidatorOrDefault<TModel>();

        Task ValidateAsync<TModel>(TModel model);
    }
}
