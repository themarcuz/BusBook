﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class PartecipazioniModel : ViaggiAgenziaModel
    {
        public PartecipazioniModel()
        {
            TitoloRelazione = "Partecipazioni di ";
        }
    }
}
