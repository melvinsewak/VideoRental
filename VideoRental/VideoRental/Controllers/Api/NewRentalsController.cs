using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VideoRental.Dtos;
using VideoRental.Models;

namespace VideoRental.Controllers.Api
{
    public class NewRentalsController : ApiController
    {

        private readonly ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> CreateNewRentals(NewRentalDto newRentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input parameters");

            var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id==newRentalDto.CustomerId);

            if (customerInDb == null)
                return BadRequest("Customer Id is invalid");

            if(newRentalDto.MovieIds==null)
                return BadRequest("No Movie Ids are provided");

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            if (movies == null)
                return BadRequest("All Movie Ids are invalid");

            if(movies.Count!=newRentalDto.MovieIds.Count)
                return BadRequest("One or more Movie Ids are invalid");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest($"Movie with Id {movie.Id} is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer=customerInDb,
                    Movie=movie,
                    DateRented=DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            await _context.SaveChangesAsync();

            return Ok();

        }

        
    }
}