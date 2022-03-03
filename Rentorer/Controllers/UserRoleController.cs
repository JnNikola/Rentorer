using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rentor.Models;
using Rentorer.Models;
using Rentorer.ViewModels;

namespace Rentorer.Controllers
{
    public class UserRoleController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        public UserRoleController()
        {
            _context = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageAll)]
        public ActionResult UserRole()
        {
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();

            var viewModel = new AssignRoleFormViewModel()
            {
                Roles = roles,
                Users = users
            };

            return View("AssignForm", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageAll)]
        public ActionResult Assign(string userId, string roleId, string roleName)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (role == null || user == null)
            {
                return HttpNotFound();
            }

            if (userManager.IsInRole(userId, roleName))
            {
                return Json(new { success = false, responseText = "This user is already in that role!" }, JsonRequestBehavior.AllowGet);
            }

            userManager.AddToRole(userId, roleName);

            //return Content(string.Format("User: {0},   Role: {1}", userId, roleId));
            return Json(new { success = true, responseText = "Role successfully assigned!" }, JsonRequestBehavior.AllowGet);
        }
    }
}