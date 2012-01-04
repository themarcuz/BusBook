using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class RegistrationView
    {
        [Display(Name = "Si desidera registrare anche l'Agenzia?")]
        public bool RegisterAgency { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Nome")]
        public string UtenteNome { get; set; }

        [Display(Name = "Cognome")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string UtenteCognome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Password")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]
        public string UtentePassword { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Conferma la password")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]
        public string UtenteRepeatPassword { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Email")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Indirizzo email non valido")]
        public string UtenteEmail { get; set; }

        [Display(Name = "Telefono")]
        public string UtenteTelefono { get; set; }

        public string AgenziaNome { get; set; }
        public string AgenziaRagioneSociale { get; set; }
        public string AgenziaPIva { get; set; }
        public GeoLocation AgenziaLocation { get; set; }
        public String AgenziaTelefono { get; set; }
        public String AgenziaFax { get; set; }
        public string AgenziaEmail { get; set; }
    }
}
