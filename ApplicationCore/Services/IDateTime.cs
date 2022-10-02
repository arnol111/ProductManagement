using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
