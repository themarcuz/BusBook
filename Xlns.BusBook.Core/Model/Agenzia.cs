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
        [StringLength(50, ErrorMessage = "Il campo può essere lungo al massimo 50 caratteri")]
        public virtual string Nome { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Il campo può essere lungo al massimo 100 caratteri")]
        public virtual string RagioneSociale { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Il campo può essere lungo al massimo 13 caratteri")]
        public virtual string PIva { get; set; }

        public virtual GeoLocation Location { get; set; }

        [Display(Name="Tel.")]
        public virtual String Telefono { get; set; }

        [Display(Name = "Fax")]
        public virtual String Fax { get; set; }
        
        [Required]
        public virtual string Email { get; set; }
        
    }
}
