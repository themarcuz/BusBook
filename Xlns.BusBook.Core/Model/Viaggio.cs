using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Model
{
    public class Viaggio : ModelEntity
    {

        public virtual String Nome { get; set; }
        public virtual DateTime DataPartenza { get; set; }
        public virtual DateTime DataChiusuraPrenotazioni { get; set; }
        public virtual IList<Tappa> Tappe { get; set; }

    }
}
