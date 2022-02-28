using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentorer.Models;

namespace Rentorer.Controllers
{
    [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
    public class RentalsController : Controller
    {
        // GET: Rentals
        [Authorize(Roles = RoleName.CanManageAll)]
        public ActionResult New()
        {
            return View();
        }
    }
}