using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractenOpvolging.Models.ContractenModels
{
    [Table("Klanten")]
    public class Klant
    {
        [Key]
        public int KlantID { get; set; }
        [Required]
        public string Naam { get; set; }
        public string Contactpersoon { get; set; }
        public string Telefoon { get; set; }
        public string Email { get; set; }
        //public Adres Adres { get; set; } to-Do add Complex Type Adres

        //test voor nieuwe branch

    }
}
