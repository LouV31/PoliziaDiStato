using System.ComponentModel.DataAnnotations.Schema;

namespace PoliziaDiStato.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodiceFiscale { get; set; }
        [NotMapped]
        public int TotaleNumeroVerbali { get; set; }
        [NotMapped]
        public int TotalePuntiDecurtati { get; set; }

        public double TotaleImporti { get; set; }

        public string NomeCompleto
        {
            get
            {
                return Nome + " " + Cognome + " " + "(" + CodiceFiscale + ")";
            }
        }

        public Anagrafica() { }

    }
}