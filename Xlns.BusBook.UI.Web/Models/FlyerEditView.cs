using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class FlyerEditView
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio!")]
        [Display(Name = "Titolo")]
        [StringLength(50, ErrorMessage = "Il titolo dev'essere al massimo 50 caratteri")]
        public String Titolo { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(500, ErrorMessage = "La descrizione dev'essere al massimo 500 caratteri")]
        public String Descrizione { get; set; }

        public int idAgenzia { get; set; }
        public int Id { get; set; }

        [Remote("CheckViaggiSelected", "Flyer", ErrorMessage = "Selezionare almeno un viaggio!")]
        public int hasViaggiSelected { get; set; }

        public FlyerEditView(Flyer flyer)
        {
            Id = flyer.Id;
            Titolo = flyer.Titolo;
            Descrizione = flyer.Descrizione;
            idAgenzia = flyer.Agenzia.Id;
        }

        public FlyerEditView(){}

        public String RedirectOnSave { get; set; }
    }
}