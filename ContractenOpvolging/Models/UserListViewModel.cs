using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Models
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
