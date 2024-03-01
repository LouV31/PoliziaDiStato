using System;
using System.ComponentModel.DataAnnotations;

namespace PoliziaDiStato.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [Required(ErrorMessage = "La data della violazione è obbligatoria")]

        [DataType(DataType.DateTime)]
        [Display(Name = "Data Violazione")]
        public DateTime DataViolazione { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data Trascrizione")]
        public DateTime DataTrascrizione { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Nome Agente")]
        public string NomeAgente { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]

        public double Importo { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Punti Decurtati")]
        public int PuntiDecurtati { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Persona")]
        public int FK_IdAnagrafica { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Display(Name = "Tipo Violazione")]
        public int FK_IdViolazione { get; set; }


        public string IndirizzoViolazione { get; set; }


        public Verbale() { }
    }
}