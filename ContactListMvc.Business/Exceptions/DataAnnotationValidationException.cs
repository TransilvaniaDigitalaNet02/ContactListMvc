using ContactListMvc.Business.Models;

namespace ContactListMvc.Business.Exceptions
{
    public sealed class DataAnnotationValidationException : BaseException
    {
        public DataAnnotationValidationException(IEnumerable<ValidationError> errors) 
            : base(string.Join(";", errors.Select(e => e.ErrorMessage)))
        {
            Errors = new List<ValidationError>(errors);
        }

        public override ErrorType ErrorType => ErrorType.InputValidationError;

        public IReadOnlyList<ValidationError> Errors { get; }
    }
}
