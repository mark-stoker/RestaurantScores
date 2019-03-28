using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.PatternSegments;
using Microsoft.EntityFrameworkCore.Internal;
using RestaurantScores.Models;
using RestaurantScores.Managers;

namespace RestaurantScores.Controllers
{
	public class HomeController : Controller
	{
		private List<Restaurant> _restaurants;

        public IActionResult Index()
        {
            return View();
        }

		//TODO implement IOS as per Phil's suggestion
		//This should clean up this method a bit
	    [HttpPost]
	    public IActionResult Restaurant(Restaurant restaurant)
	    {
			var webSearchManager = new WebSearchManager();
		    var restaurants = webSearchManager.BingWebSearch(restaurant.Name);

			//Todo call the Resuts action method below passing in the list of Restaurants
			var databaseManager = new DatabaseManager();
		    var reviewers = databaseManager.GetSearchData();
		    var scrapingManager = new ScrapingManager();
		    var results = new List<ResultsViewModel>();
			
		    //TODO need to deal with possible system null reference exception here
		    foreach (var res in restaurants)
		    {
			    foreach (var review in reviewers)
			    {
				    if (review.WebAddress.Contains(res.Url))
				    {
					    //TODO reviewers will need to be updated with custom url
					    scrapingManager.RenderScrapingResults(res, scrapingManager, reviewers, results);
					}
			    }
		    }

		    return View("Results", results);
		}

		public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
