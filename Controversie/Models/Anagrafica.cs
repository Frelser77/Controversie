using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Controversie.Models
{
    public class Anagrafica
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int IdAnagrafica { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Indirizzo { get; set; }

        [StringLength(255)]
        [Display(Name = "Cittá")]
        public string Citta { get; set; }

        [StringLength(5)]
        public string CAP { get; set; }

        [StringLength(16)]
        [Display(Name = "Codice Fiscale")]
        public string Cod_Fisc { get; set; }
    }
}