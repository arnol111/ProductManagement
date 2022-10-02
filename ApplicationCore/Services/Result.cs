﻿using System.Collections.Generic;
using System;
using System.Linq;

namespace ApplicationCore.Services
{
    public class Result
    {
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }

}