using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractenOpvolging.Models.ContractenModels
{
    [Table("Contracten")]
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }
        public string Opzegtermijn { get; set; }
        [DataType(DataType.MultilineText)]
        public string Randvoorwaarden { get; set; }
        [DataType(DataType.Currency)]
        public decimal Tarief { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Kosten { get; set; }
        public Verlenging Verlenging { get; set; }

    }

    public enum Verlenging
    {
        Green,
        Yellow,
        Orange,
        Red
    }
}
