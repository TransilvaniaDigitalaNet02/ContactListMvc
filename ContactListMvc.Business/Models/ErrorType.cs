namespace ContactListMvc.Business.Models
{
    public enum ErrorType
    {
        InputValidationError = 0,

        AuthenticationError,

        AuthorizationError,

        ResourceNotFoundError,

        CurrentStateDoesntAllowChangeError,

    }
}
