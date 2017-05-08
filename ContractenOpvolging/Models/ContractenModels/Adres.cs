using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContractenOpvolging.Models.ContractenModels
{
    [ComplexType]
    public class Adres
    {
        [Column("Straat")]
        public string Straat { get; set; }
        [Column("HuisNr")]
        public string HuisNr { get; set; }
        [Column("PostCode")]
        public string Postcode { get; set; }
        [Column("Gemeente")]
        public string Gemeente { get; set; }
    }
}
