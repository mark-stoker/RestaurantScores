using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.PatternSegments;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using RestaurantScores.Models;
using RestaurantScores.Managers;
using RestaurantScores.Models.Interfaces;

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
		//This will also sure classes are not tightly coupled
	    [HttpPost]
	    public IActionResult Search(Search search)
	    {
			var webSearchManager = new WebSearchManager();
		    var webSearchResults = webSearchManager.BingWebSearch(search.SearchString);
			var databaseManager = new DatabaseManager();
		    var reviewersScrapingDetails = databaseManager.GetScrapingData();
		    var scrapingManager = new ScrapingManager();
		    var results = scrapingManager.ScrapeRestaurantReviewSites(webSearchResults, scrapingManager, reviewersScrapingDetails);

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
