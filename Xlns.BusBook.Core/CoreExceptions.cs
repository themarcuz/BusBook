using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core
{
    public class NonPubblicabileException : Exception
    {
        public NonPubblicabileException(String message)
            : base(message)
        {
        }
    }
}
