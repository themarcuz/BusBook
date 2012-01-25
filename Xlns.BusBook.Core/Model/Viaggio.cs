using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Viaggio : ModelEntity
    {

        [Required]
        [Display(Name="Nome")]        
        public virtual String Nome { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(500, ErrorMessage="La descrizione dev'essere al massimo 500 caratteri")]
        public virtual String Descrizione { get; set; }

        [Display(Name = "Data di partenza")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime DataPartenza { get; set; }

        [Display(Name = "Data di chiusura delle prenotazioni")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]        
        public virtual DateTime DataChiusuraPrenotazioni { get; set; }

        public virtual IList<Tappa> Tappe { get; set; }

        public virtual Agenzia Agenzia { get; set; }

    }
}
