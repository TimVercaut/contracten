using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ContractenOpvolging.Data;

namespace ContractenOpvolging.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContractenOpvolging.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ApplicationRoleListViewModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRoleListViewModel");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractenModels.Consultant", b =>
                {
                    b.Property<int>("ConsultantID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Familienaam")
                        .IsRequired();

                    b.Property<int>("Soort");

                    b.Property<string>("Telefoon");

                    b.Property<string>("Voornaam")
                        .IsRequired();

                    b.HasKey("ConsultantID");

                    b.ToTable("Consultants");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractenModels.Contract", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConsultantID");

                    b.Property<DateTime>("EindDatum");

                    b.Property<int>("KlantID");

                    b.Property<decimal?>("Kosten");

                    b.Property<int?>("OnderKlant");

                    b.Property<string>("Opzegtermijn");

                    b.Property<string>("Randvoorwaarden");

                    b.Property<DateTime>("StartDatum");

                    b.Property<decimal?>("Tarief");

                    b.Property<int>("Verlenging");

                    b.HasKey("ContractID");

                    b.HasIndex("ConsultantID");

                    b.HasIndex("KlantID");

                    b.ToTable("Contracten");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractenModels.Klant", b =>
                {
                    b.Property<int>("KlantID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contactpersoon");

                    b.Property<string>("Email");

                    b.Property<string>("Gemeente");

                    b.Property<string>("Huisnummer");

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.Property<string>("Postcode");

                    b.Property<string>("Straat");

                    b.Property<string>("Telefoon");

                    b.HasKey("KlantID");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractenModels.OudeContracten", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginDatum");

                    b.Property<string>("Consultant");

                    b.Property<DateTime>("EindDatum");

                    b.Property<string>("Klant");

                    b.Property<decimal?>("Kosten");

                    b.Property<string>("Subklant");

                    b.Property<decimal?>("Tarief");

                    b.HasKey("Id");

                    b.ToTable("Oudecontracten");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractVerlengingViewModel", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Consultant");

                    b.Property<DateTime>("EindDatum");

                    b.Property<string>("Klant");

                    b.Property<DateTime>("NieuweEindDatum");

                    b.Property<string>("NieuweKleur");

                    b.Property<int>("VerlengKleur");

                    b.HasKey("ContractID");

                    b.ToTable("ContractVerlengingViewModel");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ResetRolesViewModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.HasKey("Id");

                    b.ToTable("ResetRolesViewModel");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.UserListViewModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("UserListViewModel");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.UserViewModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserViewModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ContractenOpvolging.Models.ContractenModels.Contract", b =>
                {
                    b.HasOne("ContractenOpvolging.Models.ContractenModels.Consultant", "Consultant")
                        .WithMany("OudeContracten")
                        .HasForeignKey("ConsultantID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractenOpvolging.Models.ContractenModels.Klant", "Klant")
                        .WithMany("Contracten")
                        .HasForeignKey("KlantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ContractenOpvolging.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ContractenOpvolging.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ContractenOpvolging.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ContractenOpvolging.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractenOpvolging.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
