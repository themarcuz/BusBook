using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DettaglioPasswordView : ModelEntity
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]
        [Display(Name = "Password")]
        [Remote("CheckPassword", "Utente", AdditionalFields="userId", ErrorMessage = "La password attuale non corrisponde con quella inserita!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]
        [Display(Name = "Nuova password")]
        public string newPassword { get; set; }
        
        [Required(ErrorMessage="Campo obbligatorio")]
        [StringLength(16, ErrorMessage = "Il campo può essere lungo al massimo 16 caratteri")]   
        [Display(Name = "Conferma la nuova password")]
        [Compare("newPassword",ErrorMessage="Le password non corrispondono!")]
        public string repeatNewPassword { get; set; }

        public int userId { get; set; }
    }
}