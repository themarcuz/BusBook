using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Model
{
    class Partecipazione : ModelEntity
    {
        public virtual Viaggio Viaggio { get; set; }
        /// <summary>
        /// Utente (rappresentante un'agenzia) che richiede la partecipazione
        /// </summary>
        public virtual Utente Utente { get; set; }
        /// <summary>
        /// data in cui viene effettuata la richiesta di partecipazione
        /// </summary>
        public virtual DateTime DataRichiesta { get; set; }
    }
}
