using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Rentor.Models;
using Rentorer.Dto;
using Rentorer.Models;

namespace Rentorer.Controllers.Api
{
    [Authorize(Roles = RoleName.CanManageAll+", "+RoleName.CanManageMovies)]
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageAll)]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            if (newRental.MovieIds.Count == 0)
                return BadRequest("No movies selected");


            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Customer is not valid");


            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (newRental.MovieIds.Count != movies.Count)
                return BadRequest("One or more movies are invalid");

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable <= 0)
                    return BadRequest("Movie not available");

                movie.NumberAvailable--;

                var rental = new Rental()
                {
                    Customer = customer,
                    DateRented = DateTime.Now,
                    Movie = movie
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}