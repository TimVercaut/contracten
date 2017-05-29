using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models.ContractenModels
{
    public class OudContract
    {
        [Key]
        public int Id { get; set; }
        public string Jaar { get; set; }
        public string Klant { get; set; }
        public string Subklant { get; set; }
        public string Consultant { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Tarief { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Kost { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }
    }
}
