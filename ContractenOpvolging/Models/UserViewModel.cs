using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
