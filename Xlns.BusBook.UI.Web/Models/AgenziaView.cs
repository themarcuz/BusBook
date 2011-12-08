using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class AgenzieView 
    {
        public List<DettaglioAgenziaView> Agenzie {get; set;}
        
        public String SearchString { get; set; }

        public String Iniziale { get; set; }

        public AgenzieView(List<Agenzia> agenzieOriginali, String s, String ini) 
        {
            Agenzie = new List<DettaglioAgenziaView>();
            SearchString = s;
            Iniziale = ini;
            foreach (var a in agenzieOriginali)
            {
                Agenzie.Add(new DettaglioAgenziaView(a, s));
            }
        }

    }
}