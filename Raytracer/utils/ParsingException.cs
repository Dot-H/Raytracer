using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class ParsingException : Exception
    {
        const string PREFIX = "ParsingException:\n";

        public ParsingException() : base() { }

        public ParsingException(string message) : base(PREFIX + message) { }

        public ParsingException(string message, System.Exception inner) : base(PREFIX + message, inner) { }

    }
}
