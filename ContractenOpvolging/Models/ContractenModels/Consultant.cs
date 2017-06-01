using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractenOpvolging.Models.ContractenModels
{
    [Table("Consultants")]
    public class Consultant
    {
        [Key]
        public int ConsultantID { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Familienaam { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public SoortConsultant Soort { get; set; }
        public List<Contract> OudeContracten { get; set; }


        public string Naam  
        {
            get
            {
                return this.Voornaam + " " + this.Familienaam;
            }
        }

    }

    public enum SoortConsultant
    {
        Vast,
        Freelance,
        Extern
    }
}
