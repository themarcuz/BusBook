using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class AllegatoViewModel
    {
        public virtual Viaggio Viaggio { get; set; }
        public virtual TipoAllegato Tipo { get; set; }
        public String Errore { get; set; }

        public enum TipoAllegato { DEPLIANT, IMMAGINE}
    }
    
}