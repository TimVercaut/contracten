using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContractenOpvolging.Models;
using ContractenOpvolging.Models.ContractenModels;

namespace ContractenOpvolging.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Contract> Contracten { get; set; }
        public DbSet<Consultant> Consultants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ContractenOpvolging.Models.UserListViewModel> UserListViewModel { get; set; }

        public DbSet<ContractenOpvolging.Models.ApplicationRoleListViewModel> ApplicationRoleListViewModel { get; set; }

        public DbSet<ContractenOpvolging.Models.UserViewModel> UserViewModel { get; set; }

    }
}
