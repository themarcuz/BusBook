using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class UtenteLoginView
    {
        [Required(ErrorMessage = "Campo obbligatorio!")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio!")]
        public String Password { get; set; }

        public String LoginErrorMessage { get; set; }
    }
}