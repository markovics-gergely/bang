using System;

namespace UserIdentity.BLL.Application.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(){}
        public InvalidParameterException(string message) : base(message){}
        public InvalidParameterException(string message, Exception innerException) : base(message, innerException){}
    }
}
