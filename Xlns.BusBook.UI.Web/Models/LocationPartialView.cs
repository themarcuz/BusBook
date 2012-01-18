using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class LocationPartialView
    {
        public GeoLocation Location { get; set; }
        public String MapWidth { get; set; }
        public String MapHeight { get; set; }                
        public string CssStyleForDataFieldDiv { get; set; }
        public string CssStyleForMapDiv { get; set; }
        public bool ShowDataFieldLegend { get; set; }
        public bool MapJsAlreadyLoaded { get; set; }

        public LocationPartialView() 
        {
            // valori di default
            MapHeight = "350px";
            MapWidth = "500px";            
            CssStyleForDataFieldDiv = "display: none";
            ShowDataFieldLegend = true;
            MapJsAlreadyLoaded = false;
        }
    }
}