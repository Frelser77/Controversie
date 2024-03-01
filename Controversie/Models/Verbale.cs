using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Controversie.Models
{
    public class Verbale
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int IdVerbale { get; set; }

        [Display(Name = "Data della Violazione")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La data violazione è obbligatoria.")]
        public DateTime DataViolazione { get; set; }

        [Display(Name = "Indirizzo Violazione")]
        [StringLength(255)]
        [Required(ErrorMessage = "L'indirizzo è obbligatorio.")]
        public string IndirizzoViolazione { get; set; }

        [Display(Name = "Nominativo Agente")]
        [StringLength(50)]
        [Required(ErrorMessage = "Il nominativo agente è obbligatorio.")]
        public string NominativoAgente { get; set; }

        [Display(Name = "Data della Trascrizione")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La data tarscrizione è obbligatoria.")]
        public DateTime DataTrascrizione { get; set; }

        [Required(ErrorMessage = "Il prezzo dell'articolo è obbligatorio.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Importo")]
        public decimal Importo { get; set; }

        [Display(Name = "Decurtamento Punti")]
        [Required(ErrorMessage = "Il decurtamento punti è obbligatorio.")]
        public int DecurtamentoPunti { get; set; }

        [Display(Name = "Anagrafica")]
        [Key]
        public int Fk_IdAnagrafica { get; set; }
        [Key]

        [Display(Name = "Tipo Violazione")]
        public int Fk_IdViolazione { get; set; }

        [Display(Name = "Pagata")]
        public bool Pagata { get; set; }
    }
}