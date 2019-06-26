using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VideoRental.Models;

namespace VideoRental.Controllers
{
    public class CustomersController : Controller
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

        [Route("Customers")]
        // GET: Customers
        public ActionResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = GetCustomerDetails(id);
            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include(c => c.MembershipType).ToList();
        }

        private Customer GetCustomerDetails(int id)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == id);
        }
    }
}