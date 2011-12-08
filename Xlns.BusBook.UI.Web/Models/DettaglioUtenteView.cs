using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DettaglioUtenteView
    {

        public String NomeAdv
        {
            get
            {
                if (string.IsNullOrEmpty(search))
                    return utente.Cognome;
                else
                {
                    var start = utente.Cognome.ToLower().IndexOf(search.ToLower());
                    if (start >= 0)
                    {
                        var originalString = utente.Cognome.Substring(start, search.Length);
                        var retString = utente.Cognome.Replace(originalString, "<span class='matchString'>" + originalString + "</span>");
                        return retString;
                    }
                    else
                        return utente.Cognome;
                }
            }
        }

        private String search;
        public Utente utente { get; set; }

        public DettaglioUtenteView(Utente u, String s)
        {
            utente = u;
            search = s;
        }
    }
}