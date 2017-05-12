using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models
{
    public class ApplicationRole:IdentityRole
    {
        public string Description { get; set; }
    }
}
