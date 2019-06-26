using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VideoRental.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using VideoRental.ViewModels;

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

        private Movie GetMovieDetails(int id)
        {
            return _context.Movies.Include(m=>m.Genre).SingleOrDefault(m => m.Id==id);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList();
        }
    }
}