using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Xlns.Google.Maps.Directions
{
    public class Services
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Output OutputType { get; private set; }
        public String GoogleWsDirectionApiAddress { get; private set; }

        private String ServiceCompleteAddress
        {
            get 
            {
                return GoogleWsDirectionApiAddress + "/" + OutputType.ToString().ToLower();
            }
        }

        public Services()
        {
            OutputType = Output.JSON;
            GoogleWsDirectionApiAddress = "http://maps.googleapis.com/maps/api/directions";
        }

        public int CalcolaDistanzaPercorsa(Request request) 
        {
            try
            {
                string requestUrl = BuildRequestUrl(request.ToString());
                logger.Debug("Url interrogazione servizi di google: {0}",requestUrl);
                WebClient service = new WebClient();
                String jsonString = service.DownloadString(requestUrl);
                JObject json = JObject.Parse(jsonString);                
                var result = json.SelectToken("routes[0].legs").Select(l => trimKm((String)l.SelectToken("distance.text"))).Sum();
                logger.Info("Calcolata distanza percorsa: {0} km", result);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Impossibile elaborare la richiesta di calcolo della distanza per questa request: {0}", request.ToString());
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        private double trimKm(String orig) {
            CultureInfo culture = new CultureInfo("en-US");
            var number = double.Parse(orig.Remove(orig.IndexOf(" km"), " km".Length), culture.NumberFormat);
            return number;
        }

        private String BuildRequestUrl(String request) 
        {
            return String.Format("{0}/{1}?{2}",
                GoogleWsDirectionApiAddress, OutputType.ToString().ToLower(), request);
        }
    }

    public enum Output 
    {
        XML, JSON
    }
}
