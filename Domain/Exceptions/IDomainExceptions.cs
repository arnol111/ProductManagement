using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public interface IDomainExceptions
    {
        class NotFoundException: Exception { }

        class InvalidDateException : Exception { }

    }
}
