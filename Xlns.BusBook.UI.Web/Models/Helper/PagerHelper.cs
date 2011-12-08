using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xlns.BusBook.UI.Web.Models
{
    public class PagerHelper
    {
        public static int calcolaNumeroDiPagine(int numeroElementi, int numeroElementiPerPagina)
        {
            int offset = 0;
            if (numeroElementi % numeroElementiPerPagina > 0) offset = 1;
            var result = (numeroElementi / numeroElementiPerPagina) + offset;
            return result;
        }
    }
}