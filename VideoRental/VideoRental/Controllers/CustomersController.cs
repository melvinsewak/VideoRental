using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VideoRental.Models;
using VideoRental.ViewModels;

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

        [Route("Customers/New")]
        public async Task<ActionResult> New()
        {
            var customerFormViewModel = new CustomerFormViewModel()
            {
                Customer=new Customer(),
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };
            return View("CustomerForm", customerFormViewModel);
        }

        [Route("Customers/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var customerFormViewModel = new CustomerFormViewModel()
            {
                Customer=_context.Customers.Include(c=>c.MembershipType).Single(c=>c.Id==id),
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };
            return View("CustomerForm", customerFormViewModel);
        }

        [Route("Customers/Save")]
        public async Task<ActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerFormViewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = await _context.MembershipTypes.ToListAsync()
                };

                return View("CustomerForm", customerFormViewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = await _context.Customers.SingleAsync(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Customers");
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