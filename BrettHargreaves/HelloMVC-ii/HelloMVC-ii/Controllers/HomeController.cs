using HelloMVC_ii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace HelloMVC_ii.Controllers
{
    public class HomeController : Controller
    {
        //Create Object Cache
        ObjectCache cache = MemoryCache.Default;
        //List of Customer
        List<Customer> customers;

        //Constructor which will be run whenever we hit any endpoint
        public HomeController()
        {
            //check if anything is in memory
            customers = cache["customers"] as List<Customer>;
            //if no customers create an empty list
            if (customers == null)
            {
                customers = new List<Customer>();
            }           
        }
        public void SaveCache()
        {
            //save the customers list in memory
            cache["customers"] = customers;
        }
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

        // Add Customer View - this controller will accept different types of calls to it
        public ActionResult AddCustomer()
        {
            return View();
        }
        //scaffolding AddCustomer page use [HttpPost] decorator because overriding with the same function name (AddCustomer)
        //- this controller will accept POST methods
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();
            //Redirect to CustomerList action
            return RedirectToAction("CustomerList");
        }


        //CustomerList scaffolding
        public ActionResult CustomerList()
        {
            return View(customers);
        }

    }
}