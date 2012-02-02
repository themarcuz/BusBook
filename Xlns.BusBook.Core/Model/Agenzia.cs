using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Agenzia : ModelEntity
    {        
        [Required]
        [Display(Name = "Nome Agenzia*")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Nome { get; set; }

        [Required]
        [Display(Name = "Ragione Sociale*")]
        [StringLength(100, ErrorMessage = "Il campo può essere lungo al massimo 100 caratteri")]
        public virtual string RagioneSociale { get; set; }

        [Required]
        [Display(Name = "Partita Iva*")]
        [StringLength(13, ErrorMessage = "Il campo può essere lungo al massimo 13 caratteri")]
        public virtual string PIva { get; set; }

        public virtual GeoLocation Location { get; set; }

        [Display(Name="Tel.")]
        public virtual String Telefono { get; set; }

        [Display(Name = "Fax")]
        public virtual String Fax { get; set; }
        
        [Required]
        [Display(Name = "Email*")]
        public virtual string Email { get; set; }

        public virtual string Skype { get; set; }

        public virtual string Facebook { get; set; }

        public virtual string Twitter { get; set; }

        public virtual IList<Utente> Utenti { get; set; }

        public virtual IList<Viaggio> Viaggi { get; set; }
       
    }
}
