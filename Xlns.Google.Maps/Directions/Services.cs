using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

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

        public String CalcolaDistanzaPercorsa(Request request) 
        {
            try
            {
                string requestUrl = BuildRequestUrl(request.ToString());
                logger.Debug("Url interrogazione servizi di google: {0}",requestUrl);
                var response = WebRequest.Create(requestUrl).GetResponse();

                return "";
            }
            catch (Exception ex)
            {
                string msg = String.Format("Error {0}", null);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
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
