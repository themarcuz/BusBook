using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Model
{
    public class Tappa : ModelEntity
    {
        public virtual TipoTappa Tipo { get; set; }
        public virtual int Ordinamento { get; set; }
        public virtual GeoLocation Location { get; set; }
    }

    public enum TipoTappa {
        PARTENZA=1, TAPPA=2, DESTINAZIONE=3
    }

}

