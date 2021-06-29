using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOB.Generator.Helpers
{
    public static class StringExtension
    {
        public static string CapitalizeFirstChar(this string input) => input switch
        {
            null          => throw new ArgumentNullException(nameof(input)),
            { Length: 1 } => input.ToUpper(),
            _             => $"{input.First().ToString().ToUpper()}{input.Substring(1)}"
        };
    }
}
