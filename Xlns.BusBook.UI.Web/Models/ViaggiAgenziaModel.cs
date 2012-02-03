using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public abstract class ViaggiAgenziaModel
    {
        public String TitoloRelazione { get; set; }
        public Agenzia AgenziaPartecipante { get; set; }
        public IList<Viaggio> Viaggi { get; set; }

    }
}
