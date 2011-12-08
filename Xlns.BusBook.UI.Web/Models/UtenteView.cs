using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class UtenteView
    {
        public List<DettaglioUtenteView> Utente { get; set; }

        public String SearchString { get; set; }

        public String Iniziale { get; set; }

        public UtenteView(List<Utente> utentiOriginali, String s, String ini)
        {
            Utente = new List<DettaglioUtenteView>();
            SearchString = s;
            Iniziale = ini;
            foreach (var u in utentiOriginali)
            {
                Utente.Add(new DettaglioUtenteView(u, s));
            }
        }

    }
}