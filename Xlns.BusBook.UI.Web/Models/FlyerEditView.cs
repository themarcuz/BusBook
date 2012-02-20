using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class FlyerEditView
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio!")]
        [Display(Name = "Titolo")]
        [StringLength(50, ErrorMessage = "Il titolo dev'essere al massimo 50 caratteri")]
        public virtual String Titolo { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(500, ErrorMessage = "La descrizione dev'essere al massimo 500 caratteri")]
        public virtual String Descrizione { get; set; }


       // public virtual IList<Viaggio> Viaggi { get; set; }

        public virtual int idAgenzia { get; set; }
        public virtual int Id { get; set; }

        public FlyerEditView(Flyer flyer)
        {
            Id = flyer.Id;
            Titolo = flyer.Titolo;
            Descrizione = flyer.Descrizione;
            idAgenzia = flyer.Agenzia.Id;
        }

        public FlyerEditView() { }

    }
}