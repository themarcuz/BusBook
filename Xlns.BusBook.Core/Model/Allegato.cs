using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Model
{
    public class Allegato : ModelEntity
    {
        public virtual String NomeFile { get; set; }
        public virtual byte[] RawFile { get; set; }
    }
   
}
