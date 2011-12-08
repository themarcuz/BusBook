using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.Core.Model
{
    public class Utente : ModelEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Nome { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Cognome { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Password { get; set; }
        public virtual string Telefono { get; set; }
        [Required]
        public virtual string Email { get; set; }
    }
}
