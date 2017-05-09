using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ContractenOpvolging.Models.ContractenModels
{
    [Table("Contracten")]
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        [DisplayName("Startdatum")]
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        [DisplayName("Einddatum")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }
        public string Opzegtermijn { get; set; }
        [DataType(DataType.MultilineText)]
        public string Randvoorwaarden { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Tarief { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Kosten { get; set; }
        public Verlenging Verlenging { get; set; }
        public virtual Klant Klant { get; set; }
        [Required]
        [DisplayName("Klant")]
        public int KlantID { get; set; }
        [DisplayName("Subklant")]
        public int? OnderKlant { get; set; }
        public virtual Consultant Consultant { get; set; }
        [Required]
        [DisplayName("Consultant")]
        public int ConsultantID { get; set; }

        public bool CheckValid(int maand, int jaar)
        {
            var begin = StartDatum.Ticks;
            var eind = EindDatum.Ticks;
            var test = new DateTime(jaar, maand, 15).Ticks;
            return ((begin <= test) && (test <= eind));
        }
    }

    public enum Verlenging
    {
        Green,
        Yellow,
        Orange,
        Red
    }
}
