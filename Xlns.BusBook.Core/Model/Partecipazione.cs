using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Partecipazione : ModelEntity
    {
        public virtual Viaggio Viaggio { get; set; }
        /// <summary>
        /// Utente (rappresentante un'agenzia) che richiede la partecipazione
        /// </summary>
        public virtual Utente Utente { get; set; }
        /// <summary>
        /// data in cui viene effettuata la richiesta di partecipazione
        /// </summary>
        [Display(Name = "Data richiesta partecipazione")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime DataRichiesta { get; set; }
    }
}
