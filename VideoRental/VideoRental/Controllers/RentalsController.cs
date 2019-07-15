using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoRental.Controllers
{
    public class RentalsController : Controller
    {
     
        [Route("Rentals/New")]
        public ActionResult New()
        {
            return View("NewRentalForm");
        }
    }
}