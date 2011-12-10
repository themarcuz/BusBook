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
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
    }
}