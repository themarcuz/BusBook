using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models.Helper
{
    public static class GoogleAPIHelper
    {

        // uses Google GeoCoding API to lookup Lat and Long for an indirizzo (http://code.google.com/apis/maps/documentation/geocoding/)


        public static Boolean lookupGeoCode(this GeoLocation loc)
        {
            string PostUrl = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + loc.Street + "," + loc.City + "," + loc.Nation + "&sensor=false";

            XDocument geo = XDocument.Load(PostUrl);

            // if an error occurs with the GeoCoding API return false
            try
            {
                loc.Lat = (from geocode in geo.Descendants("location")
                                   select geocode.Element("lat").Value).SingleOrDefault().ToString();

                loc.Lng = (from geocode in geo.Descendants("location")
                                   select geocode.Element("lng").Value).SingleOrDefault().ToString();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Object lookupGeoCode(String indirizzo)
        {
            string PostUrl = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + indirizzo + "&sensor=false";

            XDocument geo = XDocument.Load(PostUrl);

            // if an error occurs with the GeoCoding API return false
            try
            {
                var result = 
                    new { lat = (from geocode in geo.Descendants("location")
                                select geocode.Element("lat").Value).SingleOrDefault().ToString(),
                          lng = (from geocode in geo.Descendants("location")
                                select geocode.Element("lng").Value).SingleOrDefault().ToString()
                        };
                return result;
            }
            catch
            {
                return false;
            }
        }

    }
}