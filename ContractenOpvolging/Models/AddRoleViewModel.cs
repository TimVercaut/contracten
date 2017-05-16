using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models
{
    public class AddRoleViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "Rol")]
        public string ApplicationRoleId { get; set; }
    }
}
