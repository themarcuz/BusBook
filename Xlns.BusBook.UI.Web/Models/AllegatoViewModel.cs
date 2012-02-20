using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xlns.BusBook.UI.Web.Models
{
    public class AllegatoViewModel
    {
        public virtual int IdViaggio { get; set; }
        public virtual TipoAllegato Tipo { get; set; }

        public enum TipoAllegato { DEPLIANT, IMMAGINE}
    }
    
}