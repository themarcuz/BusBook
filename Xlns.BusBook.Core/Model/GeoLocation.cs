using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class GeoLocation
    {
        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        [Display(Name="Nazione")]
        public String Nation { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        [Display(Name = "Regione")]
        public String Region { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Il campo può essere lungo al massimo 10 caratteri")]
        [Display(Name = "Provincia")]
        public String Province { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        [Display(Name = "Città")]
        public String City { get; set; }

        [StringLength(100, ErrorMessage = "Il campo può essere lungo al massimo 100 caratteri")]
        [Display(Name = "Via")]
        public String Street { get; set; }

        [StringLength(10, ErrorMessage = "Il campo può essere lungo al massimo 10 caratteri")]
        [Display(Name = "Numero Civico")]
        public String Number { get; set; }

        [StringLength(10, ErrorMessage = "Il campo può essere lungo al massimo 10 caratteri")]
        [Display(Name = "CAP")]
        public String CAP { get; set; }

        [Required]
        [Display(Name = "Latitudine")]
        public String Lat { get; set; }

        [Required]
        [Display(Name = "Longitudine")]
        public String Lng { get; set; }

        public String IndirizzoLeggibile { 
            get 
            { 
                return String.Concat(Street, ", ", Number, " - ", CAP, " - ", City, " (", Province, ") - ", Nation); 
            } 
        }
        
    }
}