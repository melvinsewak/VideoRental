using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VideoRental.Dtos;
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
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customersInDb= await _context.Customers.Include(c => c.MembershipType).ToListAsync();
            return customersInDb.Select(Mapper.Map<Customer,CustomerDto>);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var customerInDb= await _context.Customers.Include(c=>c.MembershipType).SingleOrDefaultAsync(c=>c.Id==id);

            if (customerInDb == null)
                return NotFound();
            
            return Ok(Mapper.Map<Customer,CustomerDto>(customerInDb));
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            await  _context.SaveChangesAsync();
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddCustomer(CustomerDto customerDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto,Customer>(customerDto);
            var newCustomer=_context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Created(new Uri(Request.RequestUri + "/" + newCustomer.Id), Mapper.Map(newCustomer, customerDto));
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveCustomer(int id)
        {
            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<Customer, CustomerDto>(customerInDb));
        }
    }
}
