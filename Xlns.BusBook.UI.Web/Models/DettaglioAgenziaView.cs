using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DettaglioAgenziaView
    {
        private AgenziaManager am = new AgenziaManager();

        public String NomeAdv
        {
            get
            {
                if (string.IsNullOrEmpty(search))
                    return agenzia.Nome;
                else
                {
                    var start = agenzia.Nome.ToLower().IndexOf(search.ToLower());
                    if (start >= 0)
                    {
                        var originalString = agenzia.Nome.Substring(start, search.Length);
                        var retString = agenzia.Nome.Replace(originalString, "<span class='matchString'>" + originalString + "</span>");
                        return retString;
                    }
                    else
                        return agenzia.Nome;
                }
            }
        }

        private String search;
        public Agenzia agenzia { get; set; }

        public DettaglioAgenziaView(Agenzia a, String s)
        {
            agenzia = a;
            search = s;
        }

        public DettaglioAgenziaView(Agenzia a) : this(a, null) { }

        public int NumeroViaggiProposti { get { return am.CalcolaNumeroViaggiProposti(agenzia); } }
        public int NumeroViaggiPartecipati { get { return am.CalcolaNumeroViaggiPartecipati(agenzia); } }
        public int KmPercorsi { get { return KmViaggiPartecipati + KmViaggiProposti; } }
        public int KmViaggiProposti { get { return am.CalcolaKmViaggiProposti(agenzia); } }
        public int KmViaggiPartecipati { get { return am.CalcolaKmViaggiPartecipati(agenzia); } }
    }
}