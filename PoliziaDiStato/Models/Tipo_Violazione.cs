using System.ComponentModel.DataAnnotations;

namespace PoliziaDiStato.Models
{
    public class Tipo_Violazione
    {
        public int IdViolazione { get; set; }

        [Required(ErrorMessage = "Il campo Descrizione non può essere vuoto")]
        public string Descrizione { get; set; }

        public Tipo_Violazione() { }
    }
}