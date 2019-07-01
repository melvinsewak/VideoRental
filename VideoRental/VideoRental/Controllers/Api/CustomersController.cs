using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VideoRental.Models;

namespace VideoRental.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.Include(c => c.MembershipType).ToListAsync();
        }

        [HttpGet]
        public async Task<Customer> GetCustomer(int id)
        {
            var customerInDb= await _context.Customers.Include(c=>c.MembershipType).SingleOrDefaultAsync(c=>c.Id==id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customerInDb;
        }

        [HttpPut]
        public async Task<Customer> UpdateCustomer(int id, Customer customer)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipType = await _context.MembershipTypes.SingleOrDefaultAsync(c=>c.Id==customerInDb.MembershipTypeId);

           await  _context.SaveChangesAsync();
            return customerInDb;
        }

        [HttpPost]
        public async Task<Customer> AddCustomer(Customer customer)
        {

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var newCustomer=_context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }
    }
}
