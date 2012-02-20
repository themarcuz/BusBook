using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Flyer : ModelEntity
    {

        [Required(ErrorMessage="Il titolo è obbligatorio!")]
        [Display(Name="Titolo")]
        [StringLength(50, ErrorMessage = "Il titolo dev'essere al massimo 50 caratteri")]
        public virtual String Titolo { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(500, ErrorMessage="La descrizione dev'essere al massimo 500 caratteri")]
        public virtual String Descrizione { get; set; }


        public virtual IList<Viaggio> Viaggi { get; set; }

        public virtual Agenzia Agenzia { get; set; }           

    }
}
