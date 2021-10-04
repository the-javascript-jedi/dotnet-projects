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
        //we will recieve ID of customer and we must use the id and search our memory cache
        public ActionResult ViewCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }            
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
        //EditCustomer endpoint
        public ActionResult EditCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }
        //EditCustomer endpoint - takes a customer object
        //we replace the data inside the first customer object using the id received from the second parameter
        [HttpPost]
        public ActionResult EditCustomer(Customer customer, string Id)
        {
            //load customer from database
            var customerToEdit = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                //update customer's name and telephone no, if the customer data is found
                customerToEdit.Name = customer.Name;
                customerToEdit.Telephone = customer.Telephone;
                SaveCache();
                //redirect to CustomerList
                return RedirectToAction("CustomerList");
            }
        }
        //Load customer from db and return them to view with a confirm delete button
        public ActionResult DeleteCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        //Delete Action and remove the customer from db
        //we add the HttpPost decorator so that .net will know that this is a post request
        [HttpPost]
        //Decorator to overide the action name
        //- even though our actual result, our c# method is called ConfirmDeleteCustomer, we're telling it to expect an end point of DeleteCustomer.
        [ActionName("DeleteCustomer")]
        public ActionResult ConfirmDeleteCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Delete customer 
                customers.Remove(customer);
                return RedirectToAction("CustomerList");
            }
        }

        //CustomerList scaffolding
        
        public ActionResult CustomerList()
        {
            return View(customers);
        }

    }
}