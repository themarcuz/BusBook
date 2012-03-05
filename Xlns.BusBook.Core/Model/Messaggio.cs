using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Messaggio : ModelEntity
    {
        [Required]
        public virtual Utente Mittente { get; set; }

        [Required]
        public virtual Utente Destinatario { get; set; }

        [Required]
        public virtual String Testo { get; set; }

        [Required]
        public virtual int Stato { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public virtual DateTime? DataInvio { get; set; }
    }
}
