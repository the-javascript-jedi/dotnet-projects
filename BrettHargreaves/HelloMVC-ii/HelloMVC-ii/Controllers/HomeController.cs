using HelloMVC_ii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloMVC_ii.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //View Customer Action based on form submit
        //we will recieve Name and Telephone Data in a Customer Model type
        public ActionResult ViewCustomer(Customer postedCustomer)
        {
            //use the Customer Model(using HelloMVC_ii.Models;) and create object
            Customer customer = new Customer();
            customer.Id = Guid.NewGuid().ToString();
            customer.Name = postedCustomer.Name;
            customer.Telephone = postedCustomer.Telephone;
            //return a view with customer object
            return View(customer);
        }

        // Add Customer View
        public ActionResult AddCustomer()
        {
            return View();
        }
    }
}