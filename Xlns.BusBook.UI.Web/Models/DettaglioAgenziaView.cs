using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DettaglioAgenziaView
    {

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
    }
}