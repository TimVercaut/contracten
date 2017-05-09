using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ContractenOpvolging.Models.ContractenModels;

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
        [DataType(DataType.PhoneNumber)]
        public string Telefoon { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public virtual List<Contract> Contracten { get; set; }

        public string Adres
        {
            get { return this.Straat + " " + this.Huisnummer; }
        }

    }
}
