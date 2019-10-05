using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using FluentValidation;
using FluentValidation.Results;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using models = IShopify.Core.Exceptions;

namespace IShopify.DomainServices.Validation
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IComponentContext _componentContext;
        public ValidatorFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public IValidator<TModel> GetValidatorOrDefault<TModel>()
        {
            IValidator<TModel> model;

            if(_componentContext.TryResolve(out model))
            {
                return model;
            }

            return null;
        }

        public async Task ValidateAsync<TModel>(TModel model)
        {
            var validator = GetValidatorOrDefault<TModel>();

            if(validator.IsNull())
            {
                throw new InvalidOperationException($"No validator found for {typeof(TModel).Name}");
            }

            var validationResult = await validator.ValidateAsync(model);

            if(!validationResult.IsValid)
            {
                throw new models.ValidationException(ToModelValidationResult(validationResult.Errors, $"validation failed for {typeof(TModel).Name}"));
            }
        }

        private ModelValidationResult ToModelValidationResult(IList<ValidationFailure> validationFailures, string message)
        {
            var validationResult = new ModelValidationResult
            {
                Message = message,
                Errors = new List<ValidationError>()
            };

            foreach (var failure in validationFailures)
            {
                validationResult.Errors.Add(new ValidationError
                {
                    ErrorMessage = failure.ErrorMessage,
                    PropertyName = failure.PropertyName
                });
            }

            return validationResult;
        }
    }
}
