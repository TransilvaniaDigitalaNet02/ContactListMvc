﻿namespace ContactListMvc.Business.Models
{
    public sealed class ValidationError
    {
        public ValidationError(IReadOnlyList<string> properties, string errorMessage)
        {
            Properties = properties;
            ErrorMessage = errorMessage;
        }

        public IReadOnlyList<string> Properties { get; }

        public string ErrorMessage { get; }
    }
}