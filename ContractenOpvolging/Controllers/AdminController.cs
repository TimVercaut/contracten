using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContractenOpvolging.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationDbContext context;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            var model = new List<UserListViewModel>();
            foreach (var user in context.Users.OrderBy(u => u.Email))
            {
                model.Add(new UserListViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleName = GetRoleName(user.Id)
                });
            }
            return View(model);
        }

        public IActionResult Roles()
        {
            var model = new List<ApplicationRoleListViewModel>();
            model = roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                Id = r.Id,
                Description = r.Description,
                RoleName = r.Name
            }).ToList();
            return View(model);
        }

        public async Task<IActionResult> AddRole(string Id)
        {
            var model = new AddRoleViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            if (!string.IsNullOrEmpty(Id))
            {
                var user = await userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    model.Email = user.Email;
                    model.Id = user.Id;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string id, AddRoleViewModel user)
        {
            var rolNaam = await roleManager.FindByIdAsync(user.ApplicationRoleId);

            if (ModelState.IsValid)
            {
                var _user = userManager.FindByIdAsync(user.Id);
                await userManager.AddToRoleAsync(await _user, rolNaam.NormalizedName);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddRole", user.Id);
            }
        }

        public IActionResult ResetRoles(string id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                var model = new ResetRolesViewModel
                {
                    Id = user.Id,
                    Email = user.Email
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Reset(string id)
        {
            var geb = User.Identity.Name;
            if (id != null)
            {
                var user = await userManager.FindByIdAsync(id);
                var admin = "Admin"; //Hardcoded -- toDo: sql error workaround?
                var User = "User";
                await userManager.RemoveFromRoleAsync(user,admin);
                await userManager.RemoveFromRoleAsync(user, User);
            }            
            return RedirectToAction("Index");
        }

        public string GetRolID(string userId)
        {
            return (from userRole in context.UserRoles
                    where userRole.RoleId == userId
                    select userRole.RoleId).FirstOrDefault();
        }


        public string GetRoleName(string userId)
        {
            return (from role in context.Roles
                    join user in context.UserRoles on role.Id equals user.RoleId
                    where user.UserId == userId
                    select role.NormalizedName).FirstOrDefault();

        }

        public IActionResult OudeContracten()
        {
            var model = new List<OudeContractenListViewModel>();
            //foreach(var oudContract in context.Oudecontracten.OrderBy(c => c.Consultant.Familienaam))
            //{
            //    model.Add(new OudeContractenListViewModel
            //    {
            //        Consultant = oudContract.Consultant.Naam,
            //        ArchiefContracten = oudContract.OudeContracten.ToList()
            //    });
            //}
            return View(model);
        }
    }
}