using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class InvalidDateProductException : Exception
    {
        public InvalidDateProductException()
        {
        }

        public InvalidDateProductException(string message)
            : base(message)
        {
        }

        public InvalidDateProductException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
