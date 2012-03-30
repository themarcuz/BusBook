using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class ViaggioSearch 
    {
        [Display(Name = "Nome")]
        public String SearchString { get; set; }

        [Display(Name = "Data di partenza minima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMin { get; set; }
        [Display(Name = "Data di partenza massima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMax { get; set; }

        [Display(Name = "Prezzo minimo")]
        public Decimal? PrezzoMin { get; set; }
        [Display(Name = "Prezzo massimo")]
        public Decimal? PrezzoMax { get; set; }

        public String idDivToUpdate { get; set; }
    }
}