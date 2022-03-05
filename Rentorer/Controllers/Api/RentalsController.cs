using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Rentor.Models;
using Rentorer.Dto;
using Rentorer.Models;

namespace Rentorer.Controllers.Api
{
    [Authorize(Roles = RoleName.CanManageAll + "," + RoleName.CanManageMovies)]
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetRentals(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var rentalDtos = _context.Rentals
                .Include(r => r.Movie)
                .Where(r => r.Customer.Id == id)
                .ToList()
                .Select(Mapper.Map<Rental, RentalDto>);

            return Ok(rentalDtos);
        }

        [HttpPost]
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

        [HttpPatch]
        public IHttpActionResult ReturnRental(int id)
        {
            var rental = _context.Rentals.Include(r => r.Movie).SingleOrDefault(r => r.Id == id);
            if (rental == null)
                return NotFound();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == rental.Movie.Id);

            if (rental.DateReturned != null || movie == null)
                return BadRequest();

            rental.DateReturned = DateTime.Now;
            movie.NumberAvailable++;

            _context.SaveChanges();

            return Ok();
        }
    }
}
