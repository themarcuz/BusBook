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
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Cognome")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Cognome { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Username")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Username { get; set; }
        
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Password { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Email { get; set; }
        
        public virtual Agenzia Agenzia { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1} {2} - {3}", Id, Nome, Cognome, Username);
        }
    }
}
