using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractenOpvolging.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var model = new List<UserListViewModel>();
            model = userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                Email = u.Email
            }).ToList();
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

        public IActionResult AddRole(string Id)
        {
            var user = (from u in userManager.Users
                        where u.Id == Id
                        select u).FirstOrDefault();
            if (user != null)
            {
                ViewBag.Lijst = new SelectList(roleManager.Roles.ToList());

                var model = new UserViewModel()
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
    }
}