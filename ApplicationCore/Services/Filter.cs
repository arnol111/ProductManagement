using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class Filter
    {
        public string PropertyName { set; get; }
        public FilterType Type { get; set; }
        public string Value { set; get; }
    }

    public enum FilterType
    {
        Equals = 1,
        NotEquals = 2
    }
}
