using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentor.Models;
using Rentorer.Models;

namespace Rentorer.Controllers
{
    [Authorize(Roles = RoleName.CanManageAll +","+RoleName.CanManageMovies)]
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerRentals()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var rental = _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .SingleOrDefault(r => r.Id == id);

            if (rental == null)
            {
                return HttpNotFound();
            }

            return View(rental);
        }

    }
}