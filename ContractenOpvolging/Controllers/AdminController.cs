using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContractenOpvolging.Services;

namespace ContractenOpvolging.Controllers
{
    [Authorize]
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

        //public async Task<IActionResult> Index()
        //{
        //    var model = new List<UserListViewModel>();
        //    model = await userManager.Users.Select(u => new UserListViewModel
        //    {
        //        Id = u.Id,
        //        Email = u.Email,
        //    }).ToListAsync();

        //    return View(model);
        //}

        public IActionResult Index()
        {
            var model = new List<UserListViewModel>();
            foreach (var user in context.Users)
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

            var rolNaam = (from roles in context.Roles
                           where roles.Id == user.ApplicationRoleId
                           select roles.NormalizedName).FirstOrDefault();

            if (ModelState.IsValid)
            {
                var _user = userManager.FindByIdAsync(user.Id);
                //if (!context.Users.Find(user.Id).Roles.ToString().Contains(rolNaam))
                await userManager.AddToRoleAsync(await _user, rolNaam);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddRole", user.Id);
            }
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
    }
}