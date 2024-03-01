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

        [Display(Name = "Data Violazione")]
        [DataType(DataType.Date)]
        public DateTime DataViolazione { get; set; }

        [Display(Name = "Indirizzo Violazione")]
        [StringLength(255)]
        public string IndirizzoViolazione { get; set; }

        [Display(Name = "Nominativo Agente")]
        [StringLength(50)]
        public string NominativoAgente { get; set; }

        [Display(Name = "Data Trascrizione")]
        [DataType(DataType.Date)]
        public DateTime DataTrascrizione { get; set; }

        [Required(ErrorMessage = "Il prezzo dell'articolo è obbligatorio.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Importo")]
        public decimal Importo { get; set; }

        [Display(Name = "Decurtamento Punti")]
        public int DecurtamentoPunti { get; set; }

        [Display(Name = "Anagrafica")]
        public int Fk_IdAnagrafica { get; set; }

        [Display(Name = "Tipo Violazione")]
        public int Fk_IdViolazione { get; set; }

        [Display(Name = "Pagata")]
        public bool Pagata { get; set; }
    }
}