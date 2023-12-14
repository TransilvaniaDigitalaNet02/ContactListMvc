using ContactListMvc.Business.Exceptions;
using ContactListMvc.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactListMvc.Business.Validation
{
    internal static class DataAnnotationsValidator
    {
        public static void Validate<T>(T model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            List<ValidationResult> validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            if (!Validator.TryValidateObject(model, validationContext, validationResults))
            {
                throw new DataAnnotationValidationException(
                    validationResults.Select(
                        r => new ValidationError(r.MemberNames.ToList(), r.ErrorMessage ?? "Unspecified validation error.")));
            }
        }
    }
}
