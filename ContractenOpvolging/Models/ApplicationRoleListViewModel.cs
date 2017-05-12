using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContractenOpvolging.Models
{
    public class ApplicationRoleListViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }
    }
}
