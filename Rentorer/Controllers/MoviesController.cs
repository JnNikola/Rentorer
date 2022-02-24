using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Rentor.Models;
using Rentor.ViewModels;
using Rentorer.Models;
using Rentorer.ViewModels;


namespace Rentorer.Controllers
{
    
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context= new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageAll) || User.IsInRole(RoleName.CanManageMovies))
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

            var movie = _context.Movies.Include(m=> m.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        //passing full NewMovieViewModel to have IsEditing field
        public ActionResult Save(NewMovieViewModel mv)
        {

            var movie = mv.Movie;

            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel()
                {
                    IsEditing = mv.IsEditing,
                    Genres = _context.Genres.ToList(),
                    Movie = movie
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded= DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieFromDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieFromDb.Name = movie.Name;
                movieFromDb.GenreId = movie.GenreId;
                movieFromDb.NumberInStock = movie.NumberInStock;
                movieFromDb.ReleaseDate = movie.ReleaseDate;

                TryUpdateModel(movieFromDb);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movieFromDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieFromDb==null)
            {
                return HttpNotFound();
            }

            NewMovieViewModel viewModel = new NewMovieViewModel()
            {
                Movie = movieFromDb,
                Genres = _context.Genres.ToList(),
                IsEditing = true
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult New()
        {
            IEnumerable<Genre> genres = _context.Genres.ToList();
            NewMovieViewModel viewModel = new NewMovieViewModel()
            {
                Genres = genres,
                IsEditing = false
            };

            return View("MovieForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageAll + ", " + RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie!= null)
            {
                _context.Movies.Remove(movie);
            }
            
            _context.SaveChanges();
                
            return RedirectToAction("Index", "Movies");
        }

        [Route("movies/released/{year:regex(\\d{2}):range(1800, 2030)}/{month:regex(\\d{2}):range(1, 12)}")]
        public ContentResult ByReleaseDate(int year, int month)
        {
            return Content(String.Format("Month={0}, Year={1}", month, year));
        }

    }
}