
using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using VideoRental.Dtos;
using VideoRental.Models;

namespace VideoRental.Controllers.Api
{
    public class MoviesController : ApiController
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

        // GET api/<controller>
        public async Task<IEnumerable<MovieDto>> GetMovies(string query =null)
        {
            var moviesQuery = _context.Movies.Include(m=>m.Genre).Where(m=>m.NumberAvailable>0);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesInDb = await moviesQuery.ToListAsync();
            return moviesInDb.Select(Mapper.Map<Movie, MovieDto>);
        }

    }
}