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
        public DbSet<OudContract> OudeContracten { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<UserListViewModel> UserListViewModel { get; set; }
        public DbSet<ApplicationRoleListViewModel> ApplicationRoleListViewModel { get; set; }
        public DbSet<UserViewModel> UserViewModel { get; set; }
        public DbSet<ResetRolesViewModel> ResetRolesViewModel { get; set; }
        public DbSet<ContractVerlengingViewModel> ContractVerlengingViewModel { get; set; }

    }
}
