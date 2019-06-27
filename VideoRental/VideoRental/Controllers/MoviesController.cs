using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VideoRental.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using VideoRental.ViewModels;
using System;
using System.Data.Entity.Validation;

namespace VideoRental.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        [Route("Movies")]
        public ActionResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        [Route("Movies/New")]
        public async Task<ActionResult> New()
        {
            var movieFormViewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genre = await _context.Genres.ToListAsync()
            };
            return View("MovieForm",movieFormViewModel);
        }

        [Route("Movies/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var movieFormViewModel = new MovieFormViewModel
            {
                Movie = await _context.Movies.Include(m => m.Genre).SingleAsync(m=>m.Id==id),
                Genre = await _context.Genres.ToListAsync()
            };
            return View("MovieForm", movieFormViewModel);
        }

        [Route("Movies/Save")]
        public async Task<ActionResult> Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genre = await _context.Genres.ToListAsync()
                };
                return View("MovieForm",viewModel);
            }

            if (movie.Id == 0)
            {
                movie.AdditionDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = await _context.Movies.Include(m => m.Genre).SingleAsync(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.AdditionDate = DateTime.Now;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("Index", "Movies");
        }

        private IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList();
        }
    }
}