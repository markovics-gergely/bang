using System;

namespace Bang.BLL.Application.Exceptions
{
    public class NotEnoughPlayerException : Exception
    {
        public NotEnoughPlayerException(){}
        public NotEnoughPlayerException(string message) : base(message){}
        public NotEnoughPlayerException(string message, Exception innerException) : base(message, innerException){}
    }
}
