using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Rentor.Models;
using Rentorer.Migrations;
using Rentorer.Models;
using Rentorer.ViewModels;

namespace Rentorer.Controllers
{
    public class CustomersController : Controller
    {


        private ApplicationDbContext _context;
        // GET: Customers

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageAll))
            {
                return View("Index");
            }
            return View("IndexReadOnly");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var customer = _context.Customers.Include(c=>c.MembershipTypeType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes,
                IsEditing = false
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        //passing full NewCustomerViewModel to have IsEditing field
        public ActionResult Save(NewCustomerViewModel cu)
        {
            var customer = cu.Customer;

            if (!ModelState.IsValid)
            {
                NewCustomerViewModel viewModel = new NewCustomerViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id==0)
            {
                 _context.Customers.Add(customer);
            }
            else
            {
                var customerFromDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerFromDb.Birthday = customer.Birthday;
                customerFromDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerFromDb.MembershipTypeId = customer.MembershipTypeId;
                customerFromDb.Name = customer.Name;
                TryUpdateModel(customerFromDb);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            var customerFormViewModel = new NewCustomerViewModel()
            {
                Customer = customer, 
                MembershipTypes = _context.MembershipTypes.ToList(),
                IsEditing = true
            };
            return View("CustomerForm", customerFormViewModel);
        }

        
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer!= null)
            {
                _context.Customers.Remove(customer);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}