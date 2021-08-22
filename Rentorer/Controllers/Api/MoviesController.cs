using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Rentor.Models;
using Rentorer.Dto;

namespace Rentorer.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDto>);
        }

        //GET: /api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //PUT: /api/movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movieInDb.Id), movieDto);

        }

        //DELETE: /api/movies/id
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
