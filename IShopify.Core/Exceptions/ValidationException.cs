using IShopify.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Exceptions
{
    [Serializable]
    public class ValidationException : AppException
    {
        public ValidationException(ModelValidationResult validationResult, string message = null)
            : base(message ?? "Validation failed for the given model")
        {
            ArgumentGuard.NotNull(validationResult, nameof(validationResult));

            ValidationResult = validationResult;
        }

        public ModelValidationResult ValidationResult { get; set; }

        public override System.Collections.IDictionary Data
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { "ValidationResult", ValidationResult }
                };
            }
        }
    }

    [Serializable]
    public class ModelValidationResult
    {
        public string Message { get; set; }

        public IList<ValidationError> Errors { get; set; }
    }

    [Serializable]
    public class ValidationError
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
