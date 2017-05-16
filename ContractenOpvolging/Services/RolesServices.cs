using ContractenOpvolging.Data;
using ContractenOpvolging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractenOpvolging.Services
{
    public class RolesServices
    {
        private readonly RoleManager<ApplicationDbContext> roleManager;
        private readonly IdentityDbContext context;

        public RolesServices(RoleManager<ApplicationDbContext> roleManager, IdentityDbContext context )
        {
            this.roleManager = roleManager;
            this.context = context;
        }

        public string GetRole(string id)
        {
            var rolId = (from userRole in context.UserRoles
                         where userRole.UserId == id
                         select userRole.RoleId).FirstOrDefault();

            var rolNaam = (from roles in context.Roles
                           where roles.Id == rolId
                           select roles.NormalizedName).FirstOrDefault();

            return rolNaam;
        }

    }
}
