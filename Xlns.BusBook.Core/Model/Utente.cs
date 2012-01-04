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
        [StringLength(18, ErrorMessage = "Il campo può essere lungo al massimo 18 caratteri")]
        public virtual string Password { get; set; }
        public virtual string Telefono { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Indirizzo email non valido")]
        public virtual string Email { get; set; }
    }
}
