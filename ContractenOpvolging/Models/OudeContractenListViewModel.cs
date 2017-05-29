using ContractenOpvolging.Models.ContractenModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models
{
    public class OudeContractenListViewModel
    {
        public string Jaar { get; set; }
        public Contract ArchiefContract { get; set; }
    }
}
