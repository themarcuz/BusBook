using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Utente : ModelEntity
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Cognome { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Username { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Password { get; set; }
        
        public virtual Agenzia Agenzia { get; set; }
    }
}
