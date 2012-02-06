using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.Google.Maps.Directions
{
    /// <summary>
    /// Per maggiori dettagli sui parametri, vedere http://code.google.com/intl/it-IT/apis/maps/documentation/directions/#RequestParameters
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Indica al servizio se il dispositivo chiamante è dotato di sensori di geolocalizzazione o meno (defualt = false)
        /// </summary>
        public bool Sensor { get; set; }

        /// <summary>
        /// E' possibile impostare delle restrizioni ai percorsi suggeriti (da considerarsi come preferenze e non come divieti assoluti)
        /// </summary>
        public Avoidable? Avoid { get; set; }

        /// <summary>
        /// Indica il mezzo di trasporto utilizzato
        /// http://code.google.com/intl/it-IT/apis/maps/documentation/directions/#TravelModes
        /// </summary>
        public TravelMode? Mode { get; set; }

        /// <summary>
        /// Chiede al servizio di restituire anche percorsi alternativi
        /// </summary>
        public bool? Alternatives { get; set; }

        /// <summary>
        /// Indica l'unità di misura da utilizzare
        /// </summary>
        public UnitsSystem? Units { get; set; }

        /// <summary>
        /// Indica la region di preferenza, utilizzando il formato ccTLD <see cref="http://en.wikipedia.org/wiki/CcTLD" />
        /// </summary>
        public String Region { get; set; }

        /// <summary>
        /// Può essere un indirizzo (Italia, Firenze, via Nazionale 13) oppure coordinate geografiche
        /// scritte come Lat,Lng (44.4070624,8.933988900000031): nel secondo caso attenzione a non inserire spazi
        /// </summary>
        public String Origin { get; set; }

        /// <summary>
        /// Può essere un indirizzo (Italia, Firenze, via Nazionale 13) oppure coordinate geografiche
        /// scritte come Lat,Lng (44.4070624,8.933988900000031): nel secondo caso attenzione a non inserire spazi
        /// </summary>
        public String Destination { get; set; }

        /// <summary>
        /// Lista di elementi geografici opzionali: possono essere indirizzi (Italia, Firenze, via Nazionale 13) oppure coordinate geografiche
        /// scritte come Lat,Lng (44.4070624,8.933988900000031): nel secondo caso attenzione a non inserire spazi
        /// </summary>
        public IList<String> Waypoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origine">
        /// Può essere un indirizzo (Italia, Firenze, via Nazionale 13) oppure coordinate geografiche
        /// scritte come Lat,Lng (44.4070624,8.933988900000031): nel secondo caso attenzione a non inserire spazi
        /// </param>
        /// <param name="destinazione">
        /// Può essere un indirizzo (Italia, Firenze, via Nazionale 13) oppure coordinate geografiche
        /// scritte come Lat,Lng (44.4070624,8.933988900000031): nel secondo caso attenzione a non inserire spazi
        /// </param>
        /// <param name="presenzaSensoreLocalizzazione">Indica al servizio se il dispositivo chiamante è dotato di sensori di geolocalizzazione o meno (defualt = false)</param>
        public Request(String origine, String destinazione, bool presenzaSensoreLocalizzazione = false)
        {
            Origin = origine;
            Destination = destinazione;
            Sensor = presenzaSensoreLocalizzazione;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sezione di dati obbligatori
            sb.Append("origin=").Append(Origin).Append("&destination=").Append(Destination).Append("&sensor=").Append(Sensor.ToString().ToLower());
            //aggiunta eventuali waypoints
            if (Waypoints != null && Waypoints.Count > 0)
            {
                sb.Append("&waypoints=");               
                for (int i = 0; i < Waypoints.Count ; i++)
                {
                    sb.Append(Waypoints[i]);
                    if (i != (Waypoints.Count - 1)) sb.Append("|");
                }
            }
            //sezioni dati opzionali
            if (Mode.HasValue) sb.Append("&mode=").Append(Mode.Value.ToString().ToLower());
            if (Avoid.HasValue) sb.Append("&avoid=").Append(Avoid.Value.ToString().ToLower());
            if (Units.HasValue) sb.Append("&units=").Append(Units.Value.ToString().ToLower());
            if (Alternatives.HasValue) sb.Append("&alternatives=").Append(Alternatives.Value.ToString().ToLower());
            if (!String.IsNullOrEmpty(Region)) sb.Append("&region=").Append(Region.ToLower());

            return sb.ToString();
        }

    }

    public enum Avoidable
    {
        TOLLS, HIGHWAYS
    }

    public enum TravelMode
    {
        DRIVING, WALKING, BICYCLING
    }

    public enum UnitsSystem
    {
        METRIC, IMPERIAL
    }

}
