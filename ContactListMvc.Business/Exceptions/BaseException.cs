﻿using ContactListMvc.Business.Models;

namespace ContactListMvc.Business.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message)
            : base(message)
        { }

        protected BaseException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public abstract ErrorType ErrorType { get; }
    }
}
