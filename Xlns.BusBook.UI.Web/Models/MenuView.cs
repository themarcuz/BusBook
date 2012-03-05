using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;

namespace Xlns.BusBook.UI.Web.Models
{
    public class MenuView
    {
        public Utente Utente { get; set; }
        public IList<Messaggio> Messaggi { get; set; }

        public MenuView(Utente utente)
        {
            Utente = utente;
            var mr = new MessaggioRepository();
            Messaggi = mr.GetMessaggiUnreadByDestinatario(utente.Id);
        }
    }
}
