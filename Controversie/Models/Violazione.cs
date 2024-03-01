using System.ComponentModel.DataAnnotations;

namespace Controversie.Models
{
    public class Violazione
    {
        [Key]
        public int IdViolazione { get; set; }

        public string Descrizione { get; set; }

        public bool Contestabile { get; set; }

        public int PuntiDecurtati { get; set; }

        public Violazione()
        {
        }
        public Violazione(int idViolazione, string descrizione, int decurtamentoPunti)
        {
            IdViolazione = idViolazione;
            Descrizione = descrizione;
            PuntiDecurtati = decurtamentoPunti;
        }
    }
}