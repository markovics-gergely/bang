using System;

namespace Bang.BLL.Application.Exceptions
{
    public class NotEnoughCardException : Exception
    {
        public NotEnoughCardException(){}
        public NotEnoughCardException(string message) : base(message){}
        public NotEnoughCardException(string message, Exception innerException) : base(message, innerException){}
    }
}
