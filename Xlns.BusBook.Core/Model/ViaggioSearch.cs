﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class ViaggioSearch 
    {
        public String SearchString { get; set; }

        public DateTime? DataPartenzaMin { get; set; }
        public DateTime? DataPartenzaMax { get; set; }

        public GeoLocation PassaDa { get; set; }
        public TipoSearch PassaDaTipoSearch { get; set; }
        public GeoLocation ArrivaA { get; set; }
        public TipoSearch ArrivaATipoSearch { get; set; }

        public Decimal? PrezzoMin { get; set; }
        public Decimal? PrezzoMax { get; set; }


        public bool onlyPubblicati { get; set; }

        public ViaggioSearch()
        {
            PassaDaTipoSearch = TipoSearch.Città;
            ArrivaATipoSearch = TipoSearch.Città;
        }
    }

    public enum TipoSearch
    {
        Città,
        Provincia,
        Regione,
        Nazione
    }
}