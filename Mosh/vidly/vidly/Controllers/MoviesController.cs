using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.Models;
using vidly.ViewModels;
namespace vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek The View"
            };
            var Movies = new List<Movie>
            {
                new Movie{ Name = "Transformers" },
                new Movie{ Name = "The pursuit of happyness" },
            };
            var customers = new List<Customer>
            {
                new Customer{ Name="Customer 1"},
                new Customer{ Name="Customer 2"}
            };
            // create a view model
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Movies=Movies,
                Customers = customers
            };


            return View(viewModel);
        }

        // GET: Movies/Movies
        public ActionResult Movies()
        {           
            var Movies = new List<Movie>
            {
                new Movie{ Name = "Transformers" },
                new Movie{ Name = "The pursuit of happyness" },
            };           
            // create a view model
            var viewModel = new RandomMovieViewModel
            {                
                Movies = Movies,               
            };


            return View(viewModel);
        }
    }

}