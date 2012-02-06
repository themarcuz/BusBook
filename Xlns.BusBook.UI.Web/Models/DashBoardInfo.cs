using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DashBoardInfo
    {
        public Utente Utente { get; set; }
        public DettaglioAgenziaView DettaglioAgenzia { get; set; }

        public DashBoardInfo(Utente utente)
        {
            Utente = utente;
            DettaglioAgenzia = new DettaglioAgenziaView(Utente.Agenzia);
        }
    }
}