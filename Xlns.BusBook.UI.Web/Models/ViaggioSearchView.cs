using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class ViaggioSearchView
    {
        [Display(Name = "Nome")]
        public String SearchString { get; set; }

        [Display(Name = "Data di partenza minima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMin { get; set; }
        [Display(Name = "Data di partenza massima")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime? DataPartenzaMax { get; set; }

        public GeoLocationView PassaDa { get; set; }
        public TipoSearch PassaDaTipoSearch { get; set; }
        public GeoLocationView ArrivaA { get; set; }
        public TipoSearch ArrivaATipoSearch { get; set; }

        [Display(Name = "Prezzo minimo")]
        [Range(typeof(decimal), "0","1000000", ErrorMessage="Il Prezzo minimo deve essere compreso tra 0 e 1000000 di euro")]
        public decimal? PrezzoMin { get; set; }
        [Display(Name = "Prezzo massimo")]
        [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Il Prezzo massimo deve essere compreso tra 0 e 1000000 di euro")]
        public decimal? PrezzoMax { get; set; }

        public String idDivToUpdate { get; set; }

        public bool onlyPubblicati { get; set; }

        public bool isFlyersSearch { get; set; }

        public ViaggioSearchView()
        {
            PassaDaTipoSearch = TipoSearch.Città;
            ArrivaATipoSearch = TipoSearch.Città;
        }
    }
}