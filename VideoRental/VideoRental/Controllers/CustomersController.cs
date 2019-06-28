using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            var listOfCustomers = GetCustomers();
            return View(listOfCustomers);
        }

        [Route("Customers/New")]
        public async Task<ActionResult> New()
        {
            var membershipTypesInDb = await _context.MembershipTypes.ToListAsync();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypesInDb.Select(Mapper.Map<MembershipType, MembershipTypeViewModel>)
            };
            return View("CustomerForm", viewModel);
        }

        [Route("Customers/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var customerInDB = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDB == null)
                return HttpNotFound();

            var membershipTypesInDb = await _context.MembershipTypes.ToListAsync();
            var viewModel = new CustomerFormViewModel();

            Mapper.Map(customerInDB, viewModel);
            viewModel.MembershipTypes = membershipTypesInDb.Select(Mapper.Map<MembershipType, MembershipTypeViewModel>);

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customers/Save")]
        public async Task<ActionResult> Save(CustomerFormViewModel customerFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypesInDb = await _context.MembershipTypes.ToListAsync();
                customerFormViewModel.MembershipTypes = membershipTypesInDb.Select(Mapper.Map<MembershipType, MembershipTypeViewModel>);
                return View("CustomerForm", customerFormViewModel);
            }

            if (customerFormViewModel.Id == 0)
            {
                var customerTobeAdded = Mapper.Map<CustomerFormViewModel, Customer>(customerFormViewModel);
                _context.Customers.Add(customerTobeAdded);
            }
            else
            {
                var customerToBeUpdated = await _context.Customers.SingleOrDefaultAsync(c => c.Id == customerFormViewModel.Id);

                if (customerToBeUpdated == null)
                    return HttpNotFound();

                Mapper.Map(customerFormViewModel, customerToBeUpdated);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                var customerRrrorMessage="Following validation error occurred:\n";
                foreach (var error in e.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        customerRrrorMessage += validationError.ErrorMessage+"\n";
                    }
                }
                Debug.WriteLine(customerRrrorMessage);
            }

            return RedirectToAction("Index","Customers");
        }

        private IEnumerable<CustomerIndexViewModel> GetCustomers()
        {
            return _context.Customers.Include(c => c.MembershipType).ToList().Select(Mapper.Map<Customer,CustomerIndexViewModel>);
        }
    }
}