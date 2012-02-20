using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
<<<<<<< HEAD
using Xlns.BusBook.Core.Model;
=======
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c

namespace Xlns.BusBook.UI.Web.Models
{
    public class AllegatoViewModel
    {
<<<<<<< HEAD
        public virtual Viaggio Viaggio { get; set; }
        public virtual TipoAllegato Tipo { get; set; }
        public String Errore { get; set; }
=======
        public virtual int IdViaggio { get; set; }
        public virtual TipoAllegato Tipo { get; set; }
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c

        public enum TipoAllegato { DEPLIANT, IMMAGINE}
    }
    
}