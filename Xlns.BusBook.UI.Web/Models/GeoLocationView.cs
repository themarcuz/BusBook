using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xlns.BusBook.UI.Web.Models
{
    public class GeoLocationView
    {
        public String Nation { get; set; }
        public String Region { get; set; }
        public String Province { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String Number { get; set; }
        public String CAP { get; set; }
        public String Lat { get; set; }
        public String Lng { get; set; }

        public bool isEmpty()
        {
            return String.IsNullOrEmpty(Nation) && String.IsNullOrEmpty(Region) && String.IsNullOrEmpty(Province) &&
                    String.IsNullOrEmpty(City) && String.IsNullOrEmpty(Street) && String.IsNullOrEmpty(Number) &&
                    String.IsNullOrEmpty(CAP) && String.IsNullOrEmpty(Lat) && String.IsNullOrEmpty(Lng);
        }
    }
}