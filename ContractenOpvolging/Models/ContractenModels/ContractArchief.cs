using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractenOpvolging.Models.ContractenModels
{
    public class ContractArchief
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Jaar { get; set; }
        public List<Contract> OudeContracten { get; set; }
    }
}
