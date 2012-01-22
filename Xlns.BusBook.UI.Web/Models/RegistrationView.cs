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
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Conferma la password")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]
        public string UtenteRepeatPassword { get; set; }

        public Agenzia Agenzia { get; set; }
        public Utente Utente { get; set; }
        public RegistrationView()
        {
            this.Agenzia = new Agenzia();
        }
    }
}
