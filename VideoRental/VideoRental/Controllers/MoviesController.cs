using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VideoRental.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using VideoRental.ViewModels;
using System;
using System.Data.Entity.Validation;
using AutoMapper;
using System.Diagnostics;
using System.Net;
using System.Web;

namespace VideoRental.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

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
            var viewModel = GetMovies();
            return View(viewModel);
        }

        [Route("Movies/New")]
        public async Task<ActionResult> New()
        {
            var genresInDb = await _context.Genres.ToListAsync();
            var viewModel = new MovieFormViewModel();
            viewModel.Genres = genresInDb.Select(Mapper.Map<Genre, GenreViewModel>);
            return View("MovieForm", viewModel);
        }

        [Route("Movies/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var genresInDb = await _context.Genres.ToListAsync();
            var viewModel = new MovieFormViewModel();
            var movieInDb = await _context.Movies.Include(m => m.Genre).SingleAsync(m => m.Id == id);
            viewModel.Genres = genresInDb.Select(Mapper.Map<Genre,GenreViewModel>);
            Mapper.Map(movieInDb, viewModel);
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Movies/Save")]
        public async Task<ActionResult> Save(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var genresInDb = await _context.Genres.ToListAsync();
                viewModel.Genres = genresInDb.Select(Mapper.Map<Genre, GenreViewModel>);
                return View("MovieForm",viewModel);
            }

            if (viewModel.Id == 0)
            {
                var movieTobeAdded = Mapper.Map<MovieFormViewModel,Movie>(viewModel);
                movieTobeAdded.AdditionDate= DateTime.Now;
                _context.Movies.Add(movieTobeAdded);
            }
            else
            {
                var movieInDb = await _context.Movies.Include(m => m.Genre).SingleAsync(m => m.Id == viewModel.Id);
                Mapper.Map(viewModel, movieInDb);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                var movieRrrorMessage = "Following validation error occurred:\n";
                foreach (var error in e.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        movieRrrorMessage += validationError.ErrorMessage + "\n";
                    }
                }
                Debug.WriteLine(movieRrrorMessage);
            }

            return RedirectToAction("Index", "Movies");
        }

        [Route("Movies/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieInDb = await _context.Movies.SingleOrDefaultAsync(m=>m.Id==id);

            if (movieInDb == null)
                return HttpNotFound();

            _context.Movies.Remove(movieInDb);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");

        }

        private IEnumerable<MovieIndexViewModel> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList().Select(Mapper.Map<Movie,MovieIndexViewModel>);
        }
    }
}