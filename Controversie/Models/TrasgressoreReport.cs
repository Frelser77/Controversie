using System.ComponentModel.DataAnnotations;

namespace Controversie.Models
{
    public class TrasgressoreReport
    {
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Totale")]

        public int Totale { get; set; }  // TotaleVerbali o TotalePuntiDecurtati, a seconda del contesto
    }
}